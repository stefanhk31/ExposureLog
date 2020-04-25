using ExposureLog.iOS.Services;
using ExposureLog.Services;
using Ninject.Modules;


namespace ExposureLog.iOS.Modules
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