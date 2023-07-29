namespace XChangeAPI.Models.DB
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public char status { get; set; }
        public string createdOn { get; set; }
        public int createdBy { get; set; }
        public string modifiedOn { get; set; }
        public int modifiedBy { get; set; }
        public string passwordhash { get; set; }
    }
}
