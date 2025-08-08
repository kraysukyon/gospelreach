namespace GospelReachCapstone.Models
{
    public class Member
    {
        //Attributes
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; } 
        public string Contact { get; set; }
        public string Gender { get; set; } = "Male"; //Default Gender
        public DateOnly Birthdate { get; set; } = DateOnly.FromDateTime(DateTime.Now); // Default to today's date
        public int Age => GetAge(); // Computed property for age
        public string Classification { get; set; } = "Soldier"; // Default classification
        public string Status { get; set; } = "Active"; // Default status

        //Methods

        //Calculate Age
        public int GetAge()
        {
            int age = DateTime.Now.Year - Birthdate.Year;
            DateTime bday = Birthdate.ToDateTime(TimeOnly.MinValue);

            if (bday > DateTime.Now.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        //Construct Full Name
        public string GetFullName()
        {
            return $"{FirstName} {(MiddleName != null ? MiddleName + " " : "")}{LastName}";
        }

    }
}
