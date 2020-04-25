using ExposureLog.Models;
using ExposureLog.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    class NewEntryViewModel : BaseValidationViewModel
    {
        private readonly ILocationService _locService;

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
            _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        
        async Task Save()
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

            await NavService.GoBack();
        }
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;


        public NewEntryViewModel(INavService navService, ILocationService locService)
            : base(navService)
        {
            _locService = locService;

            Date = DateTime.Today;
            RiskRating = 1;
        }

        public override async void Init()
        {
            try
            {
                var coords = await _locService.GetCoordinatesAsync();
                Latitude = coords.Latitude;
                Longitude = coords.Longitude;
            } 
            catch (Exception)
            {
                //TODO: handle exceptions from location service
            }
        }
    }
}
