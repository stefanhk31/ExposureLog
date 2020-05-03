using System;


namespace ExposureLog.Services
{
    public interface IAuthService
    {
        void SignInAsync(string clientId, Uri authUrl, Uri callbackUrl, Action<string> tokenCallback, Action<Exception> errorCallback);
    }
}
