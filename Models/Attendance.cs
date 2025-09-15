namespace GospelReachCapstone.Models
{
    public class Attendance
    {
        public string Id { get; set; }
        public string ScheduleId { get; set; }
        public string Department { get; set; }
        public DateOnly Date { get; set; }
        public int TotalAttendee { get; set; } = 0;
        public int TotalVisitors { get; set; } = 0;
        public int Present { get; set; } = 0;
        public int Absent { get; set; } = 0;
        public bool isCompleted { get; set; } = false;
    }
}
