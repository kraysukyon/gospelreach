namespace GospelReachCapstone.Models
{
    public class AttendanceMemberRecord
    {
        public string Id { get; set; }
        public string AttendanceId { get; set; }
        public string MemberId { get; set; }
        public bool IsPresent { get; set; }
    }
}
