using Akavache;
using ExposureLog.Services;
using ExposureLog.ViewModels;
using Ninject.Modules;
using System;

namespace ExposureLog.Modules
{
    class ExposureLogCoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();

            var exposureLogService = new ExposureLogDataService(new Uri("https://exposurelog.azurewebsites.net"));
            Bind<IExposureLogDataService>()
                .ToMethod(x => exposureLogService)
                .InSingletonScope();

            Bind<IBlobCache>().ToConstant(BlobCache.LocalMachine);
        }
    }
}
