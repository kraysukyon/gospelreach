namespace GospelReachCapstone.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverRole { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
    }
}
