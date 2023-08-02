using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Logic
{
    public class TickerLogic : ITickerLogic
    {
        private readonly IExchangeRateService _exchange;
        private readonly ILogger<TickerLogic> _log;
        private readonly ICacheService _cache;
        private readonly IHistoryLogic _history;

        public TickerLogic(IExchangeRateService exchangeservice, ILogger<TickerLogic> log, ICacheService cache, IHistoryLogic history)
        {
            _exchange = exchangeservice;
            _log = log;
            _cache = cache;
            _history = history;
        }


        public async Task<Ticker> GetExchangeRate(string curr1, string curr2)
        {
            var cacheData = await _cache.GetData<Ticker>($"ticker{curr1}{curr2}");
            if (cacheData != null)
            {
                return cacheData;
            }

            var rate = await _exchange.GetExchangeRateFromAPI(curr1, curr2);
            cacheData = new Ticker
            {
                name = $"{curr1}/{curr2}",
                checkedOn = DateTime.UtcNow,
                currency1 = curr1,
                currency2 = curr2,
                exchangerate = rate


            };

            await _cache.SetData($"ticker{curr1}{curr2}", cacheData, DateTimeOffset.Now.AddMinutes(30));

            return cacheData;
        }

        public async Task<float?> Exchange(string curr1, string curr2, float amt, int userID)
        {
            try
            {
                var userIsThresholded = await _history.IsUserThresholded(userID);
                if (userIsThresholded) return null;

                await _history.PostHistory(new History
                {
                    ticker = $"{curr1}/{curr2}",
                    user = userID,
                    timestamp = DateTime.UtcNow,
                    status = 'A'
                });
                var exchangerate = await GetExchangeRate(curr1, curr2);
                return amt * exchangerate.exchangerate;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }


        }





    }
}
