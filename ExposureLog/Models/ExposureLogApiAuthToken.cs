namespace ExposureLog.Models
{
    public class ExposureLogApiAuthToken
    {
        public ExposureLogApiUser User { get; set; }
        public string AuthenticationToken { get; set; }
    }
}
