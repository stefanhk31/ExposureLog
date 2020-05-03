using ExposureLog.Models;
using ExposureLog.Services;

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


        public DetailViewModel(INavService navService)
            : base(navService)
        {
        }

        public override void Init(ExposureLogEntry paramter)
        {
            Entry = paramter;
        }
    }
}
