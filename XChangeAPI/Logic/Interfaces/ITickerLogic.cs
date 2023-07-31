using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic.Interfaces
{
    public interface ITickerLogic
    {
        Task<float> Exchange(Currency curr1, Currency curr2, float amt);
        Task<Ticker> GetExchangeRate(Currency curr1, Currency curr2);
        Task<IEnumerable<Ticker>> GetTickers();
        Task UpdateExchangeRate(Currency curr1, Currency curr2, float rate);
    }
}