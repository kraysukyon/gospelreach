using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace GospelReachCapstone.Services
{
    public class NotificationService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        public NotificationService(IJSRuntime js, HttpClient http)
        {
            _js = js;
            _http = http;
        }

        public async Task<NotificationResult> SendEmailsAsync(List<string> emailList, string subject, string body)
        {
            var email = new { To = emailList, Subject = subject, Body = body };

            try
            {
                var response = await _http.PostAsJsonAsync("https://localhost:7114/api/email/send", email);

                if (response.IsSuccessStatusCode)
                {
                    return new NotificationResult { Success = true };
                }
                else
                {
                    return new NotificationResult { Success = false };
                }
            }
            catch (Exception ex)
            {
                return new NotificationResult { Success = false, Error = ex.Message };
            }
        }

        
    }

    public class NotificationResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
