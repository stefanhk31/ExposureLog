using ExposureLog.Modules;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using ExposureLog.Views;
using Ninject;
using Ninject.Modules;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace ExposureLog
{
    public partial class App : Application
    {
        private bool IsSignedIn => !string.IsNullOrWhiteSpace(Preferences.Get("apitoken", ""));

        public IKernel Kernel { get; set; }


        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            Kernel = new StandardKernel(
                new ExposureLogCoreModule(),
                new ExposureLogNavModule());
            Kernel.Load(platformModules);

            var dataService = Kernel.Get<IExposureLogDataService>();
            dataService.AuthorizedDelegate = OnSignIn;
            dataService.UnauthorizedDelegate = SignOut;

            SetMainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=d26fd9d2-22fd-4a6b-bd0d-215d76a9f101;"
                + "android=873ec2fd-01a2-4e02-a8a5-2a7a90409168;", 
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetMainPage()
        {
            var mainPage = IsSignedIn
                ? new NavigationPage(new MainPage())
                {
                    BindingContext = Kernel.Get<MainViewModel>()
                }
                : new NavigationPage(new SignInPage())
                {
                    BindingContext = Kernel.Get<SignInViewModel>()
                };
            var navService = Kernel.Get<INavService>() as NavService;
            navService.Navigation = mainPage.Navigation;
            MainPage = mainPage;
        }

        private void OnSignIn(string accessToken)
        {
            Preferences.Set("apitoken", accessToken);
            SetMainPage();
        }

        private void SignOut()
        {
            Preferences.Remove("apitoken");
            SetMainPage();
        }
    }
}
