using ExposureLog.Services;
using ExposureLog.ViewModels;
using ExposureLog.Views;
using Ninject.Modules;


namespace ExposureLog.Modules
{
    public class ExposureLogNavModule : NinjectModule
    {
        public override void Load()
        {
            var navService = new NavService();

            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

            Bind<INavService>()
                .ToMethod(x => navService)
                .InSingletonScope();
        }
    }
}
