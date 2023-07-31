using XChangeAPI.Models.DB;

namespace XChangeAPI.Data.Interfaces
{
    public interface ICurrencyData
    {
        Task<IEnumerable<Currency>> GetCurrencies();
    }
}