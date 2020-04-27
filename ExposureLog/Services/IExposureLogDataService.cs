using ExposureLog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public interface IExposureLogDataService
    {
        Task<IList<ExposureLogEntry>> GetEntriesAsync();
        Task<ExposureLogEntry> AddEntryAsync(ExposureLogEntry entry);
    }
}
