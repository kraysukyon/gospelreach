using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class NotifServices
    {
        private readonly IJSRuntime _js;
        public NotifServices(IJSRuntime js)
        {
            _js = js;
        }

        //Send Message
        public async Task<NotfResult> SendMessageAsync(Notification notification)
        {
            try
            {
                var result = await _js.InvokeAsync<NotfResult>("firebaseAuth.sendMessage", notification.ChatRoomId, notification);
                return result;
            }
            catch(JSException ex)
            {
                return new NotfResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch(Exception ex)
            {
                return new NotfResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }


    }

    public class NotfResult
    {
        public bool Success { get; set; }
        public List<Notification> Data { get; set; }
        public string Error { get; set; }
    }
}
