using ExposureLog.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public interface IExposureLogDataService
    {
        Action<string> AuthorizedDelegate { get; set; }
        Action UnauthorizedDelegate { get; set; }
        Task AuthenticateAsync(string idProvider, string idProviderToken);
        void Unauthenticate();
        Task<IList<ExposureLogEntry>> GetEntriesAsync();
        Task<ExposureLogEntry> AddEntryAsync(ExposureLogEntry entry);
    }
}
