namespace GospelReachCapstone.Models
{
    public class Accounts
    {
        public string id { get; set; } // Firebase document ID
        public string memberId { get; set; }
        public string role { get; set; }
        public string status { get; set; } = "Active";
    }
}
