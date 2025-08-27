namespace GospelReachCapstone.Models
{
    public class Finance
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; } // "Income" or "Expense"
        public string CategoryId { get; set; }
        public string AttendanceId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
