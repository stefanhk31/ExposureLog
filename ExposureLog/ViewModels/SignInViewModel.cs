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
            _authService.SignInAsync(Constants.facebookAppKey,
                new Uri(Constants.facebookAuthUrl),
                new Uri(Constants.facebookCallbackUrl),
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
