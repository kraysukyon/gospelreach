using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class FirebaseAuthenticationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthState _authState;

        public FirebaseAuthenticationService(IJSRuntime jSRuntime, AuthState authstate)
        {
            _jsRuntime = jSRuntime;
            _authState = authstate;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(string email, string password)
        {
            var result = await _jsRuntime.InvokeAsync<RegisterResult>("firebaseAuth.register", email, password);
            return result.Success ? (true, "Account Added Successful") : (false, result.Error);
        }

        public async Task<(bool Success, string Message)> LoginAsync(string email, string password)
        {
            var result = await _jsRuntime.InvokeAsync<LoginResult>("firebaseAuth.login", email, password);
            if (result.Success)
            {
                _authState.IsLoggedIn = true;
                _authState.UserId = result.Uid;
                _authState.Email = email;
                return (true, "Login successful");
            }
            return (false, result.Error ?? "Login failed");
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firebaseAuth.logout");
                _authState.IsLoggedIn = false;
                _authState.UserId = null;
                _authState.Email = null;
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }

        }

        private class RegisterResult
        {
            public bool Success { get; set; }
            public string Uid { get; set; }
            public string Error { get; set; }
        }

        private class LoginResult
        {
            public bool Success { get; set; }
            public string Uid { get; set; }
            public string Error { get; set; }
        }
    }

    
}
