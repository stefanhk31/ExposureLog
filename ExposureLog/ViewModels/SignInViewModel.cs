using ExposureLog.Services;
using System;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    public class SignInViewModel : BaseViewModel
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
            _authService.SignInAsync("897391277398859",
                new Uri("https://m.facebook.com/dialog/oauth"),
                new Uri("https://exposurelog.azurewebsites.net/.auth/login/facebook/callback"),
                tokenCallback: async token =>
                {
                    await _exposureLogService.AuthenticateAsync("facebook", token);
                },
                errorCallback: e =>
                {
                    // TODO: Handle invalid authentication here
                });
        }
    }
}
