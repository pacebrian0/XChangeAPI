using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using XChangeAPI.Models.DB;
using XChangeAPI.Models.DTO;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly string _apiKey;
        private const string baseURL = "http://data.fixer.io/api/latest";
        private readonly ILogger<ExchangeRateService> _log;

        public ExchangeRateService(IConfiguration config, ILogger<ExchangeRateService> log)
        {

            _apiKey = config.GetSection("AppSettings:APIToken").Value ?? throw new Exception("Please set up API Token");
            _log = log;

        }

        public async Task<float> GetExchangeRateFromAPI(string curr1, string curr2)
        {
            if (curr1 is null)
            {
                throw new ArgumentNullException(nameof(curr1));
            }

            if (curr2 is null)
            {
                throw new ArgumentNullException(nameof(curr2));
            }

            var url = $"{baseURL}?access_key={_apiKey}&base={curr1}&symbols={curr2}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            FixerExchange exchangeRates = JsonConvert.DeserializeObject<FixerExchange>(result) ?? throw new Exception("Invalid JSON response in GetExchangeRate");
            return exchangeRates.rates[curr2];

        }

    }
}
