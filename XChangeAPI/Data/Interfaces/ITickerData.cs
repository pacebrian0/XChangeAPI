using XChangeAPI.Models.DB;

namespace XChangeAPI.Data.Interfaces
{
    public interface ITickerData
    {
        Task<Ticker> GetExchangeRate(string curr1, string curr2);
        Task<IEnumerable<Ticker>> GetTickers();
        Task UpdateExchangeRate(string curr1, string curr2, float amt);
    }
}