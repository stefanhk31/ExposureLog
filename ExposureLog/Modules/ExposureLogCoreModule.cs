using Akavache;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using Ninject.Modules;
using System;
using Xamarin.Essentials;

namespace ExposureLog.Modules
{
    class ExposureLogCoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<SignInViewModel>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();

            var apiAuthToken = Preferences.Get("apitoken", "");
            var exposureLogService = new ExposureLogDataService(new Uri("https://exposurelog.azurewebsites.net"), apiAuthToken);
            Bind<IExposureLogDataService>()
                .ToMethod(x => exposureLogService)
                .InSingletonScope();

            Bind<IBlobCache>().ToConstant(BlobCache.LocalMachine);

            Bind<IAuthService>().To<AuthService>().InSingletonScope();
        }
    }
}
