using ExposureLog.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ExposureLog.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavService NavService { get; private set; }
        protected IAnalyticsService AnalyticsService { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected BaseViewModel(INavService navService, IAnalyticsService analyticsService)
        {
            NavService = navService;
            AnalyticsService = analyticsService;
        }

        public virtual void Init()
        {

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel(INavService navService, IAnalyticsService analyticsService)
            : base(navService, analyticsService) {}

        public override void Init()
        {
            Init(default);
        }

        public virtual void Init(TParameter parameter)
        {

        }

    }
}
