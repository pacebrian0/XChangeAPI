using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic.Interfaces
{
    public interface ICurrencyLogic
    {
        Task<IEnumerable<Currency>> GetCurrencies();
    }
}