using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic
{
    public class CurrencyLogic : ICurrencyLogic
    {

        private readonly ICurrencyData _data;
        private readonly ILogger<CurrencyLogic> _log;

        public CurrencyLogic(ICurrencyData data, ILogger<CurrencyLogic> log)
        {
            _data = data;
            _log = log;
        }

        public async Task<IEnumerable<Currency>> GetCurrencies()
        {
            return await _data.GetCurrencies();

        }
    }
}
