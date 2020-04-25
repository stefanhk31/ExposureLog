using ExposureLog.Models;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ExposureLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        DetailViewModel ViewModel => BindingContext as DetailViewModel;
        public DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DetailViewModel.Entry))
            {
                UpdateMap();
            }
        }

        private void UpdateMap()
        {
            if (ViewModel.Entry == null)
            {
                return;
            }
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(
                ViewModel.Entry.Latitude, ViewModel.Entry.Longitude), Distance.FromMiles(.5)));
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = ViewModel.Entry.Title,
                Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
            });
        }
    }
}