using GospelReachCapstone.Models;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace GospelReachCapstone.Services
{
    public class FirebaseAuthenticationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthState _authState;
        private readonly FirestoreService _firestore;

        public FirebaseAuthenticationService(IJSRuntime jSRuntime, AuthState authstate, FirestoreService firestore)
        {
            _jsRuntime = jSRuntime;
            _authState = authstate;
            _firestore = firestore;
        }
        public async Task<(bool Success, string Message)> RegisterAsync(string email, string password, User user)
        {
            var result = await _jsRuntime.InvokeAsync<RegisterResult>("firebaseAuth.register", email, password, user);
            return result.Success ? (true, "Account Added Successful") : (false, result.Error);
        }

        public async Task<(bool Success, string Message)> LoginAsync(string email, string password)
        {
            var result = await _jsRuntime.InvokeAsync<LoginResult>("firebaseAuth.login", email, password);
            if (result.Success)
            {
                //List<Accounts> acc = await _firestore.GetAccountAsync();
                //List<Member> mem = await _firestore.GetMembersAsync();

                //var joinedList = acc.Join(mem, a => a.memberId, b => b.Id, (a,b) => new AccountsDTO
                //{
                //    Id = a.id,
                //    MemberId = a.memberId,
                //    FirstName = b.FirstName,
                //    Role = a.role,
                //    Status = a.status,
                //    Email = b.Email,
                //}).ToList();

                //var user = joinedList.FirstOrDefault(a => a.Email == email);

                //if (user.Status != "Active")
                //{
                //    return (false,"Login failed: Your account is disabled, Please contact administrator.");
                //}

                //_authState.IsLoggedIn = true;
                //_authState.UserId = result.Uid;
                //_authState.Email = email;
                //_authState.DisplayName = user?.FirstName ?? "User";
                //_authState.Role = user?.Role ?? "User";
                return (true, $"Login successful");
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
                _authState.DisplayName = "User";
                _authState.Role = "Administrator";
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
