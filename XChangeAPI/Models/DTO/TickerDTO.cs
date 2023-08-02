using XChangeAPI.Models.DB;

namespace XChangeAPI.Models.DTO
{
    public class ExchangeRateDTO
    {
        public string currency1 { get; set; }
        public string currency2 { get; set; }

    }

    public class ExchangeDTO
    {
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public float amount { get; set; }
        public int userID { get; set; }
    }
}
