using ExposureLog.Models;
using System.Threading.Tasks;


namespace ExposureLog.Services
{
    public interface ILocationService
    {
        Task<Coordinates> GetCoordinatesAsync();
    }
}
