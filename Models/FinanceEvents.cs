namespace GospelReachCapstone.Models
{
    public class FinanceEvents
    {
        public string Id { get; set; }
        public string DepartmentId { get; set; }
        public string ScheduleId { get; set; }
        public DateOnly Date { get; set; }
        public bool isCompleted { get; set; }
    }
}
