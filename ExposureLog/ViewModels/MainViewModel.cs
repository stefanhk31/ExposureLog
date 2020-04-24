using ExposureLog.Models;
using System;
using System.Collections.ObjectModel;


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


        public MainViewModel()
        {
            LogEntries = new ObservableCollection<ExposureLogEntry>
            {
                new ExposureLogEntry
                {
                    Title = "Grocery Store",
                    Notes = "Packed with lots of people, difficult to keep distance.",
                    RiskRating = 5,
                    Date = new DateTime(2020, 3, 13),
                    Latitude = 36.032667,
                    Longitude = -83.931597
                },
                new ExposureLogEntry
                {
                    Title = "Park",
                    Notes = "Only a few other walkers/joggers. One jogger passed more closely than I would've liked.",
                    RiskRating = 2,
                    Date = new DateTime(2020, 3, 21),
                    Latitude = 35.974322,
                    Longitude = -83.860789
                },
                new ExposureLogEntry
                {
                    Title = "Curbside Pickup from Brewery",
                    Notes = "Only interacted with one employee, who was wearing gloves.",
                    RiskRating = 1,
                    Date = new DateTime(2020, 4, 11),
                    Latitude = 35.990462,
                    Longitude =  -83.940735
                }
            };
        }
    }
}
