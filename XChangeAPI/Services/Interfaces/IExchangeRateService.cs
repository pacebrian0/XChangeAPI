using XChangeAPI.Models.DB;

namespace XChangeAPI.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<float> GetExchangeRateFromAPI(Currency curr1, Currency curr2);
    }
}