namespace GospelReachCapstone.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string EventName { get; set; }
        public string Tag { get; set; } = "Senior";
        public string Date { get; set; } = DateTime.Now.ToString();
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
