using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class GoogleDriveService
    {
        private readonly IJSRuntime _js;

        public GoogleDriveService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task signInAsync()
        {
            try
            {
                await _js.InvokeVoidAsync("googleDriveFunctions.init", "865916552971-6aj79obahgrlk7g9ts73hs788i2l1cgu.apps.googleusercontent.com"
                , "https://www.googleapis.com/auth/drive");
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", $"Error: {ex.Message}");
            }

        }
    }
}
