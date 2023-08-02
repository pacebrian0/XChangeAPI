namespace XChangeAPI.Models.DB
{
    public class History
    {
        public int id { get; set; }

        public string ticker { get; set; }
        public int user { get; set; }
        public DateTime timestamp { get; set; }
        public char status { get; set; }

    }
}
