namespace GospelReachCapstone.Models
{
    public class FinancialRecord
    {
        public string Id { get; set; }

        //Commong Fields
        public string Department { get; set; } = "Senior";
        public string Type { get; set; } = "Income";
        public string Category { get; set; } = "Offering";
        public decimal Amount { get; set; } = 0;
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        //Income Fields
        public string? InvoiceNumber { get; set; }
        public string? ScheduleId { get; set; }
        public string? ScheduleTitle { get; set; }
        public string? DonatorName { get; set; }

        //Expense Fields
        public string? VoucherNumber { get; set; }

        //Created
        public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateModified { get; set; }
        public string AddedByUserId { get; set; }
        public string? ModifiedByUserId { get; set; }
    }
}
