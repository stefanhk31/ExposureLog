using ExposureLog.Models;
using ExposureLog.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    public class NewEntryViewModel : BaseValidationViewModel
    {
        private readonly ILocationService _locService;
        private readonly IExposureLogDataService _exposureLogService;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title), "Title must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                Validate(() => _rating >= 1 && _rating <= 5, "Risk Rating must be between 1 and 5.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        private Command _saveCommand;
        public Command SaveCommand =>
            _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        
        async Task Save()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var newItem = new ExposureLogEntry
                {
                    Title = Title,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    Date = Date,
                    Rating = Rating,
                    Notes = Notes
                };

                await _exposureLogService.AddEntryAsync(newItem);
                await NavService.GoBack();
            } 
            finally
            {
                IsBusy = false;
            }

        }
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;


        public NewEntryViewModel(INavService navService, IAnalyticsService analyticsService, ILocationService locService, IExposureLogDataService exposureLogService)
            : base(navService, analyticsService)
        {
            _locService = locService;
            _exposureLogService = exposureLogService;
            Date = DateTime.Today;
            Rating = 1;
        }

        public override async void Init()
        {
            try
            {
                var coords = await _locService.GetCoordinatesAsync();
                Latitude = coords.Latitude;
                Longitude = coords.Longitude;
            } 
            catch (Exception e)
            {
                AnalyticsService.TrackError(e, new Dictionary<string, string>
                {
                    { "Method", "NewEntryViewModel.Init()"}
                });
            }
        }
    }
}
