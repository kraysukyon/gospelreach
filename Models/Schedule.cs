namespace GospelReachCapstone.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public string Category { get; set; } = "Event";
        public string Title { get; set; }
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Location { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
