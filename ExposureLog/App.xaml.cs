using ExposureLog.Modules;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using ExposureLog.Views;
using Ninject;
using Ninject.Modules;
using System;
using Xamarin.Forms;


namespace ExposureLog
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }


        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            Kernel = new StandardKernel(
                new ExposureLogCoreModule(),
                new ExposureLogNavModule());
            Kernel.Load(platformModules);

            SetMainPage();
        }

        private void SetMainPage()
        {
            var mainPage = new NavigationPage(new MainPage())
            {
                BindingContext = Kernel.Get<MainViewModel>()
            };
            var navService = Kernel.Get<INavService>() as NavService;
            navService.Navigation = mainPage.Navigation;
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
