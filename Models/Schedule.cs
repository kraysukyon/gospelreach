namespace GospelReachCapstone.Models
{
    public class Schedule
    { 
        public string Id { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public string DepartmentId { get; set; }
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TimeOption { get; set; } = "Custom";
        public string Location { get; set; }
        public string Description { get; set; }
        public bool HasAttendee { get; set; } = false;
        public string GroupId { get; set; }
        public bool hasAttendance { get; set; } = false;
        public bool hasFinance { get; set; } = false;
    }
}
