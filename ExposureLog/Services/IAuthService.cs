using System;


namespace ExposureLog.Services
{
    interface IAuthService
    {
        void SignInAsync(string clientId, Uri authUrl, Uri callbackUrl, Action<string> tokenCallback, Action<string> errorCallback);
    }
}
