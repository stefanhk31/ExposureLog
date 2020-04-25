using ExposureLog.Droid.Services;
using ExposureLog.Services;
using Ninject.Modules;


namespace ExposureLog.Droid.Modules
{
    public class ExposureLogPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>()
                .To<LocationService>()
                .InSingletonScope();
        }
    }
}