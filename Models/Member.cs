namespace GospelReachCapstone.Models
{
    public class Member
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; } 
        public string Contact { get; set; } 
        public string Birthdate { get; set; }
        public string DateOfSoldiership { get; set; }
        public string Classification { get; set; } = "Soldier"; // Default classification
        public string Status { get; set; } = "Active"; // Default status
        
    }
}
