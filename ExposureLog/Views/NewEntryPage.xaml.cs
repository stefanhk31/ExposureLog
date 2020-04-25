﻿using ExposureLog.Services;
using ExposureLog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ExposureLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEntryPage : ContentPage
    {
        NewEntryViewModel ViewModel => BindingContext as NewEntryViewModel;


        public NewEntryPage()
        {
            InitializeComponent();
            BindingContextChanged += Page_BindingContextChanged;
            BindingContext = new NewEntryViewModel(DependencyService.Get<INavService>());
        }

        private void Page_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var propHasErrors = (ViewModel.GetErrors(e.PropertyName)
                       as List<string>)?.Any() == true;
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Title):
                    title.LabelColor = propHasErrors
                        ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.RiskRating):
                    riskRating.LabelColor = propHasErrors
                        ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}