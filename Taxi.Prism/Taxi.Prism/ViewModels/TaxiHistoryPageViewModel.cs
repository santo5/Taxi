﻿using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Taxi.Common.Models;
using Taxi.Common.Services;
using Taxi.Prism.Helpers;

namespace Taxi.Prism.ViewModels
{
    public class TaxiHistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TaxiResponse _taxi;
        private DelegateCommand _checkPlaqueCommand;
        private bool _isRunning;
        private bool _isVisible;
        private List<TripItemViewModel> _trips;

        public TaxiHistoryPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.TaxiHistory;
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public TaxiResponse Taxi
        {
            get => _taxi;
            set => SetProperty(ref _taxi, value);
        }

        public string Plaque { get; set; }

        public DelegateCommand CheckPlaqueCommand => _checkPlaqueCommand ?? (_checkPlaqueCommand = new DelegateCommand(CheckPlaqueAsync));

        private async void CheckPlaqueAsync()
        {
            if (string.IsNullOrEmpty(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PlaqueError1,
                    Languages.Accept);
                return;
            }

            Regex regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PlaqueError2,
                    Languages.Accept);
                return;
            }
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);

                return;
            }

            Response response = await _apiService.GetTaxiAsync(Plaque, url, "api", "/Taxis");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            Taxi = (TaxiResponse)response.Result;
            IsVisible = true;
            Trips = Taxi.Trips.Where(t => t.Qualification != 0).Select(t => new TripItemViewModel(_navigationService)
            {
                EndDate = t.EndDate,
                Id = t.Id,
                Qualification = t.Qualification,
                Remarks = t.Remarks,
                Source = t.Source,
                SourceLatitude = t.SourceLatitude,
                SourceLongitude = t.SourceLongitude,
                StartDate = t.StartDate,
                Target = t.Target,
                TargetLatitude = t.TargetLatitude,
                TargetLongitude = t.TargetLongitude,
                TripDetails = t.TripDetails,
                User = t.User
            }).OrderByDescending(t => t.StartDate).ToList();

        }
    }
}
