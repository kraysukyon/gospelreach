namespace GospelReachCapstone.Models
{
    public class DepartmentMember
    {
        //Attributes
        public string MemberId { get; set; }
        public string DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }


        //Methods
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

        public string GetFullName()
        {
            return $"{FirstName} {(MiddleName != null ? MiddleName + " " : "")}{LastName}";
        }
    }
}
