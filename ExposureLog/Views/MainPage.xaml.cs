using ExposureLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExposureLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var items = new List<ExposureLogEntry>
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
            exposures.ItemsSource = items;
        }

        void New_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewEntryPage());
        }

        async void Exposures_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            var exposure = (ExposureLogEntry)e.CurrentSelection.FirstOrDefault();
            if (exposure != null)
            {
                await Navigation.PushAsync(new DetailPage(exposure));
            }
            // Clear selection 
            exposures.SelectedItem = null;
        }
    }
}