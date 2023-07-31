namespace XChangeAPI.Models.DB
{
    public class Ticker
    {
        public int id { get; set; }
        public string name { get; set; }
        public Currency currency1 { get; set; }
        public Currency currency2 { get; set; }
        public char status { get; set; }
        public DateTime createdOn { get; set; }
        public int createdBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public int modifiedBy { get; set; }
        public float exchangerate { get; set; }
        public DateTime checkedOn { get; set; }
    }
}
