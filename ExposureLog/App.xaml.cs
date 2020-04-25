using ExposureLog.Services;
using ExposureLog.ViewModels;
using ExposureLog.Views;
using Xamarin.Forms;

namespace ExposureLog
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainPage = new NavigationPage(new MainPage());
            var navService = DependencyService.Get<INavService>() as NavService;
            navService.Navigation = mainPage.Navigation;
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
