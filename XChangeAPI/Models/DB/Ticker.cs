namespace XChangeAPI.Models.DB
{
    public class Ticker
    {
        public string name { get; set; }
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public float exchangerate { get; set; }
        public DateTime checkedOn { get; set; }
    }
}
