using ExposureLog.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ExposureLog.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IExposureLogDataService _exposureLogService;

        private Command _signInCommand;
        public Command SignInCommand => _signInCommand ?? (_signInCommand = new Command(SignIn));


        public SignInViewModel(INavService navService, IAnalyticsService analyticsService, IAuthService authService, IExposureLogDataService exposureLogService)
            : base(navService, analyticsService)
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
                    AnalyticsService.TrackError(e, new Dictionary<string, string>
                        {
                            { "Method", "SignInViewModel.SignIn()"}
                        });
                });
        }
    }
}
