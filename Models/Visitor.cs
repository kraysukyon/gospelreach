namespace GospelReachCapstone.Models
{
    public class Visitor
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string InvitedByMemberId { get; set; }
        public DateOnly FirstVisitDate { get; set; }


        //Construct Full Name
        public string GetFullName()
        {
            return $"{FirstName} {(MiddleName != null ? MiddleName + " " : "")}{LastName}";
        }

    }
}
