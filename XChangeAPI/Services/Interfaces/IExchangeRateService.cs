using XChangeAPI.Models.DB;

namespace XChangeAPI.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<float> GetExchangeRateFromAPI(string curr1, string curr2);
    }
}