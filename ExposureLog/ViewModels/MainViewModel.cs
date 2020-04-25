using ExposureLog.Models;
using ExposureLog.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<ExposureLogEntry> _logEntries;
        public ObservableCollection<ExposureLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }
        public Command<ExposureLogEntry> ViewCommand => new Command<ExposureLogEntry>(async entry =>
            await NavService.NavigateTo<DetailViewModel, ExposureLogEntry>(entry));
        
        public Command NewCommand => new Command(async () =>
                await NavService.NavigateTo<NewEntryViewModel>());

        public MainViewModel(INavService navService)
            : base(navService)
        {
            LogEntries = new ObservableCollection<ExposureLogEntry>();
        }

        public override void Init()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            LogEntries.Clear();
            LogEntries.Add(new ExposureLogEntry
            {
                Title = "Grocery Store",
                Notes = "Packed with lots of people, difficult to keep distance.",
                RiskRating = 5,
                Date = new DateTime(2020, 3, 13),
                Latitude = 36.032667,
                Longitude = -83.931597
            });
            LogEntries.Add(new ExposureLogEntry
            {
                Title = "Park",
                Notes = "Only a few other walkers/joggers. One jogger passed more closely than I would've liked.",
                RiskRating = 2,
                Date = new DateTime(2020, 3, 21),
                Latitude = 35.974322,
                Longitude = -83.860789
            });
            LogEntries.Add(new ExposureLogEntry
            {
                Title = "Curbside Pickup from Brewery",
                Notes = "Only interacted with one employee, who was wearing gloves.",
                RiskRating = 1,
                Date = new DateTime(2020, 4, 11),
                Latitude = 35.990462,
                Longitude = -83.940735
            });
        }
    }
}
