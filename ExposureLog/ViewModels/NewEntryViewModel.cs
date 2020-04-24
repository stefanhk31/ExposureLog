using ExposureLog.Models;
using System;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    class NewEntryViewModel : BaseValidationViewModel
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title), "Title must be provided.");
                OnPropertyChanged();
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

        private int _riskRating;
        public int RiskRating
        {
            get => _riskRating;
            set
            {
                _riskRating = value;
                Validate(() => _riskRating >= 1 && _riskRating <= 5, "Risk Rating must be between 1 and 5.");
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
            _saveCommand ?? (_saveCommand = new Command(Save, CanSave));
        void Save()
        {
            var newItem = new ExposureLogEntry
            {
                Title = Title,
                Latitude = Latitude,
                Longitude = Longitude,
                Date = Date,
                RiskRating = RiskRating,
                Notes = Notes
            };
            // TODO: Persist entry in a later chapter
        }
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;


        public NewEntryViewModel()
        {
            Date = DateTime.Today;
            RiskRating = 1;
        }
    }
}
