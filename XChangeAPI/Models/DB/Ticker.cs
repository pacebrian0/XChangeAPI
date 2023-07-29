namespace XChangeAPI.Models.DB
{
    public class Ticker
    {
        public int id { get; set; }
        public string name { get; set; }
        public Currency currency1 { get; set; }
        public Currency currency2 { get; set; }
        public char status { get; set; }
        public string createdOn { get; set; }
        public int createdBy { get; set; }
        public string modifiedOn { get; set; }
        public int modifiedBy { get; set; }
        public float exchangerate { get; set; }
        public string checkedOn { get; set; }
    }
}
