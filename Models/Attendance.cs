namespace GospelReachCapstone.Models
{
    public class Attendance
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Service { get; set; } = "Holiness Meeting";
        public int Count { get; set; }
        public int Seekers { get; set; }
    }
}
