using ExposureLog.Services;
using System;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    class SignInViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IExposureLogDataService _exposureLogService;

        private Command _signInCommand;
        public Command SignInCommand => _signInCommand ?? (_signInCommand = new Command(SignIn));


        public SignInViewModel(INavService navService, IAuthService authService, IExposureLogDataService exposureLogService)
            : base(navService)
        {
            _authService = authService;
            _exposureLogService = exposureLogService;
        }

        private void SignIn()
        {
            _authService.SignInAsync("479446455977-gbj7dts7tqm05kc3lhkrt24th3qu71cg.apps.googleusercontent.com",
                new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                new Uri("https://exposurelog.azurewebsites.net/.auth/login/google/callback"),
                tokenCallback: async token =>
                {
                    await _exposureLogService.AuthenticateAsync("google", token);
                },
                errorCallback: e =>
                {
                    // TODO: Handle invalid authentication here
                });
        }
    }
}
