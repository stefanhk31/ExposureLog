using ExposureLog.Models;


namespace ExposureLog.ViewModels
{
    class DetailViewModel : BaseViewModel
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


        public DetailViewModel(ExposureLogEntry entry)
        {
            Entry = entry;
        }
    }
}
