using ExposureLog.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ExposureLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage(ExposureLogEntry entry)
        {
            InitializeComponent();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
        
            new Position(entry.Latitude,entry.Longitude), Distance.FromMiles(.5)));
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = entry.Title,
                Position = new Position(entry.Latitude, entry.Longitude)
            });
            title.Text = entry.Title;
            date.Text = entry.Date.ToString("M");
            riskRating.Text = $"{entry.RiskRating} out of 5 risk rating.";
            notes.Text = entry.Notes;
        }
    }
}