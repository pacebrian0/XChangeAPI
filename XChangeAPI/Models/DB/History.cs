namespace XChangeAPI.Models.DB
{
    public class History
    {
        public int id { get; set; }

        public int ticker { get; set; }
        public int user { get; set; }
        public DateTime timestamp { get; set; }
        public char status { get; set; }

    }
}
