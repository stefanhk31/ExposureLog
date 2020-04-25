using ExposureLog.ViewModels;
using Ninject.Modules;


namespace ExposureLog.Modules
{
    class ExposureLogCoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();
        }
    }
}
