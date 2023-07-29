using XChangeAPI.Models.DB;

namespace XChangeAPI.Data.Interfaces
{
    public interface ITickerData
    {
        Task<IEnumerable<Ticker>> GetTickers();
        Task<Ticker> GetExchangeRate(string curr1, string curr2);

    }
}
