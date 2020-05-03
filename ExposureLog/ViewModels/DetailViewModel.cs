using ExposureLog.Models;
using ExposureLog.Services;
using System.Collections.Generic;

namespace ExposureLog.ViewModels
{
    public class DetailViewModel : BaseViewModel<ExposureLogEntry>
    {
        private ExposureLogEntry _entry;

        public ExposureLogEntry Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }


        public DetailViewModel(INavService navService, IAnalyticsService analyticsService)
            : base(navService, analyticsService)
        {
        }

        public override void Init()
        {
            throw new EntryNotProvidedException();
        }

        public override void Init(ExposureLogEntry parameter)
        {
            AnalyticsService.TrackEvent("Entry Detail Page", new Dictionary<string, string>
                {
                    { "Title", parameter.Title }
                });
            Entry = parameter;
        }
    }
}
