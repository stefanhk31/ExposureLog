using ExposureLog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public class ExposureLogDataService : BaseHttpService, IExposureLogDataService
    {
        private readonly Uri _baseUri;
        private readonly IDictionary<string, string> _headers;

        private struct IdProviderToken
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
        }

        public Action<string> AuthorizedDelegate { get; set; }
        public Action UnauthorizedDelegate { get; set; }


        public ExposureLogDataService(Uri baseUri, string authToken)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>();
            _headers.Add("x-zumo-auth", authToken);
        }

        public async Task AuthenticateAsync(string idProvider, string idProviderToken)
        {
            var token = new IdProviderToken
            {
                AccessToken = idProviderToken
            };
            var url = new Uri(_baseUri, string.Format(".auth/login/{0}", idProvider));
            var response = await SendRequestAsync<ExposureLogApiAuthToken>(url, HttpMethod.Post, requestData: token);
            if (!string.IsNullOrWhiteSpace(response?.AuthenticationToken))
            {
                var authToken = response.AuthenticationToken;
                _headers["x-zumo-auth"] = authToken;
                AuthorizedDelegate?.Invoke(authToken);
            }
        }

        public void Unauthenticate() => UnauthorizedDelegate?.Invoke();

        public async Task<IList<ExposureLogEntry>> GetEntriesAsync()
        {
            try
            {
                var url = new Uri(_baseUri, "/api/entry");
                var response = await SendRequestAsync<ExposureLogEntry[]>(url, HttpMethod.Get, _headers);
                return response;
            }
            catch (UnauthorizedAccessException)
            {
                UnauthorizedDelegate?.Invoke();
                throw;
            }
        }

        public async Task<ExposureLogEntry> AddEntryAsync(ExposureLogEntry entry)
        {
            try
            {
                var url = new Uri(_baseUri, "/api/entry");
                var response = await SendRequestAsync<ExposureLogEntry>(url, HttpMethod.Post, _headers, entry);
                return response;
            }
            catch (UnauthorizedAccessException)
            {
                UnauthorizedDelegate?.Invoke();
                throw;
            }
        }
    }
}
