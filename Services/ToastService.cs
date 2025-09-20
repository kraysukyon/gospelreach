namespace GospelReachCapstone.Services
{
    public class ToastService
    {
        public event Action? OnChange;

        private readonly List<NotificationItem> _notifications = new();

        public IReadOnlyList<NotificationItem> Notifications => _notifications.AsReadOnly();

        public void AddNotification(string type, string title, string message, int timeoutMs = 5000)
        {
            var notification = new NotificationItem
            {
                Id = Guid.NewGuid(),
                Type = type,
                Title = title,
                Message = message,
                TimeoutMs = timeoutMs
            };

            _notifications.Add(notification);
            OnChange?.Invoke();

            // Auto-remove after timeout
            _ = RemoveAfterDelay(notification.Id, timeoutMs);
        }

        public void RemoveNotification(Guid id)
        {
            var item = _notifications.Find(n => n.Id == id);
            if (item != null)
            {
                _notifications.Remove(item);
                OnChange?.Invoke();
            }
        }

        private async Task RemoveAfterDelay(Guid id, int delay)
        {
            await Task.Delay(delay);
            RemoveNotification(id);
        }
    }
}

public class NotificationItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public string Type { get; set; } = "info"; // info, success, error
    public int TimeoutMs { get; set; } = 5000;
}
