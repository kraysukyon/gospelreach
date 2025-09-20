using GospelReachCapstone.Models;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

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
        public async Task<(bool Success, string Message)> RegisterAsync(User user)
        {
            var result = await _jsRuntime.InvokeAsync<FirebaseResult>("firebaseAuth.register", user);
            return result.Success ? (true, "Account Added Successful") : (false, result.Error);
        }

        public async Task<FirebaseResult> LoginAsync(string email, string password)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<FirebaseResult>("firebaseAuth.login", email, password);
                return result;
            }
            catch (JSException ex)
            {
                return new FirebaseResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FirebaseResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firebaseAuth.logout");
                _authState.IsLoggedIn = false;
                _authState.UserId = null;
                _authState.Email = null;
                _authState.DisplayName = "User";
                _authState.Role = "Administrator";
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }

        }

        public async Task<(bool Success, string Message)> ResetPasswordAsync(string email)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<FirebaseResult>("firebaseAuth.resetPassword", email);
                return result.Success
                    ? (true, "A password reset link has been sent to your email.")
                    : (false, result.Error ?? "Password reset failed.");
            }
            catch (Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }

        public string GeneratePassword()
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            var random = new Random();

            // Ensure at least one character from each category
            var chars = new List<char>
            {
                upper[random.Next(upper.Length)],
                lower[random.Next(lower.Length)],
                digits[random.Next(digits.Length)],
                special[random.Next(special.Length)]
            };

            // Combine all characters for remaining slots
            string allChars = upper + lower + digits + special;

            // Fill the rest of the password up to 8 characters
            for (int i = chars.Count; i < 8; i++)
            {
                chars.Add(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the final password
            return new string(chars.OrderBy(_ => random.Next()).ToArray());
        }

        public class FirebaseResult
        {
            public bool Success { get; set; }
            public string Uid { get; set; }
            public string Error { get; set; }
            public string ErrorCode { get; set; }
        }
    }

    
}
