using System;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;

namespace ExposureLog.Services
{
    class AuthService : IAuthService
    {
        public void SignInAsync(string clientId, Uri authUrl, Uri callbackUrl, Action<string> tokenCallback, Action<Exception> errorCallback)
        {
            var presenter = new OAuthLoginPresenter();
            var authenticator = new OAuth2Authenticator(clientId, "", authUrl, callbackUrl);
            authenticator.Completed += (sender, args) =>
            {
                if (args.Account != null && args.IsAuthenticated)
                {
                    tokenCallback?.Invoke(args.Account.Properties["access_token"]);
                }
                else
                {
                    errorCallback?.Invoke(new UnauthorizedAccessException("Not authenticated."));
                }
            };
            authenticator.Error += (sender, args) =>
            {
                errorCallback?.Invoke(args.Exception);
            };
            presenter.Login(authenticator);
        }
    }
}
