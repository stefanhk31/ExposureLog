using ExposureLog.Modules;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using ExposureLog.Views;
using Ninject;
using Ninject.Modules;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;


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
