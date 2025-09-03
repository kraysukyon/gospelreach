namespace GospelReachCapstone.Models
{
    public class AttendanceVisitor
    {
        public string Id { get; set; }
        public string AttendanceId { get; set; }
        public string VisitorId { get; set; }
        public string InvitedByMemberId { get; set; }
        public bool IsPresent { get; set; }
    }
}
