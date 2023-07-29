using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic
{
    public class TickerLogic:ITickerLogic
    {
        private readonly ITickerData _data;
        public TickerLogic(ITickerData data) {
            _data = data;
        }

        public async Task<IEnumerable<Ticker>> GetTickers()
        {
            return await _data.GetTickers();
        }

        public async Task<Ticker> GetExchangeRate(string curr1, string curr2)
        {
            return await _data.GetExchangeRate(curr1, curr2);
        }

        public async Task<float> Exchange(string curr1, string curr2, float amt)
        {
            var exchangerate = await GetExchangeRate(curr1, curr2);
            if((DateTime.Now - DateTime.Parse(exchangerate.checkedOn)).Minutes >= 30)
            {
                //Update Exchange Rate
            }
            return amt * exchangerate.exchangerate;

        }






    }
}
