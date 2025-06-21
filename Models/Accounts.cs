namespace GospelReachCapstone.Models
{
    public class Accounts
    {
        public string id { get; set; } // Firebase document ID
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string role { get; set; } = "Admin";
        public string stats { get; set; } = "Active";
        public string dateOfCreation { get; set; } = DateTime.Now.ToString();
    }

}
