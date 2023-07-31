using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Logic
{
    public class TickerLogic : ITickerLogic
    {
        private readonly ITickerData _data;
        private readonly IExchangeRateService _exchange;
        private readonly ILogger<TickerLogic> _log;

        public TickerLogic(ITickerData data, IExchangeRateService exchangeservice, ILogger<TickerLogic> log)
        {
            _data = data;
            _exchange = exchangeservice;
            _log = log;
        }

        public async Task<IEnumerable<Ticker>> GetTickers()
        {
            return await _data.GetTickers();
        }

        public async Task<Ticker> GetExchangeRate(Currency curr1, Currency curr2)
        {
            return await _data.GetExchangeRate(curr1.abbreviation, curr2.abbreviation);
        }

        public async Task<float> Exchange(Currency curr1, Currency curr2, float amt)
        {
            var exchangerate = await GetExchangeRate(curr1, curr2);

            if ((int)DateTime.UtcNow.Subtract(exchangerate.checkedOn).TotalMinutes >= 30)
            {
                var newExchangeRate = await _exchange.GetExchangeRateFromAPI(curr1, curr2);
                exchangerate.exchangerate = newExchangeRate;

            }
            return amt * exchangerate.exchangerate;

        }

        public async Task UpdateExchangeRate(Currency curr1, Currency curr2, float rate)
        {

        }




    }
}
