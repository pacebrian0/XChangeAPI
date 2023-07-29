namespace XChangeAPI.Models.DB
{
    public class Currency
    {
        public int id { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public char status { get; set; }
        public string createdOn { get; set; }
        public int createdBy { get; set; }
        public string modifiedOn { get; set; }
        public int modifiedBy { get; set; }

    }
}
