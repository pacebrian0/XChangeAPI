using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic.Interfaces
{
    public interface ITickerLogic
    {
        Task<float?> Exchange(string curr1, string curr2, float amt, int userID);
        Task<Ticker> GetExchangeRate(string curr1, string curr2);
    }
}