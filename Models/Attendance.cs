namespace GospelReachCapstone.Models
{
    public class Attendance
    {
        public string Id { get; set; }
        public string ScheduleId { get; set; }
        public DateOnly Date { get; set; }
        public int Count { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}
