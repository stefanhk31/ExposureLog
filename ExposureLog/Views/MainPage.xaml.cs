using ExposureLog.Models;
using ExposureLog.ViewModels;
using System;
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
            BindingContext = new MainViewModel();
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