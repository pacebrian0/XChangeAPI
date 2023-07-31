namespace XChangeAPI.Models.DTO
{
    public class FixerExchange
    {
        public string success { get; set; }
        public int timestamp { get; set; }
        public string bases { get; set; }
        public string date { get; set; }
        public Dictionary<string, float> rates { get; set; }

    }

}
