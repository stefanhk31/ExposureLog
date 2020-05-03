using Akavache;
using ExposureLog.Models;
using ExposureLog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IExposureLogDataService _exposureLogService;
        private readonly IBlobCache _cache;

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

        private Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));

        public Command SignOutCommand => new Command(_exposureLogService.Unauthenticate);

        public MainViewModel(INavService navService, IAnalyticsService analyticsService, IExposureLogDataService exposureLogService, IBlobCache cache)
            : base(navService, analyticsService)
        {
            _exposureLogService = exposureLogService;
            _cache = cache;
            LogEntries = new ObservableCollection<ExposureLogEntry>();
        }

        public override void Init()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                 _cache.GetAndFetchLatest("entries", async () => await _exposureLogService.GetEntriesAsync())
                    .Subscribe(entries =>
                    {
                        LogEntries = new ObservableCollection<ExposureLogEntry>(entries);
                    });
            }
            catch (Exception e)
            {
                AnalyticsService.TrackError(e, new Dictionary<string, string>
                {
                    { "Method", "MainViewModel.LoadEntries()"}
                }); 
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}


