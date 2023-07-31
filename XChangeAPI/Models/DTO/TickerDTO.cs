using XChangeAPI.Models.DB;

namespace XChangeAPI.Models.DTO
{
    public class ExchangeRateDTO
    {
        public Currency currency1 { get; set; }
        public Currency currency2 { get; set; }

    }

    public class ExchangeDTO
    {
        public Currency currency1 { get; set; }
        public Currency currency2 { get; set; }
        public float amount { get; set; }
    }
}
