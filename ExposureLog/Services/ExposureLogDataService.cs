using ExposureLog.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public class ExposureLogDataService : BaseHttpService, IExposureLogDataService
    {
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;


        public ExposureLogDataService(Uri baseUri)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>();
            // TODO: Add header with auth-based token in chapter 7

        }

        public async Task<IList<ExposureLogEntry>> GetEntriesAsync()
        {
            var url = new Uri(_baseUri, "/api/entry");
            var response = await SendRequestAsync<ExposureLogEntry[]>(url, HttpMethod.Get, _headers);
            return response;
        }

        public async Task<ExposureLogEntry> AddEntryAsync(ExposureLogEntry entry)
        {
            var url = new Uri(_baseUri, "/api/entry");
            var response = await SendRequestAsync<ExposureLogEntry>(url, HttpMethod.Post, _headers, entry);
            return response;
        }
    }
}
