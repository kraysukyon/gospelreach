using GospelReachCapstone.Models;
using Microsoft.JSInterop;
using System.Data;

namespace GospelReachCapstone.Services
{
    public class UserService
    {
        private readonly IJSRuntime _js;
        public UserService(IJSRuntime js)
        {
            _js = js;
        }

        //====================Functions======================

        public async Task<UserResult> GetUserAccountsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<UserResult>("firestoreFunctions.getAccounts");
                return result;
            }
            catch (JSException ex)
            {
                return new UserResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new UserResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            
        }

        //Getting the Accounts List
        public async Task<List<User>> GetAccountAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<User[]>("firestoreFunctions.getAccounts");
                return result.ToList();
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", $"Error fetching accounts: {ex.Message}");
                return new List<User>();
            }

        }

        //Update Accounts
        public async Task<UserResult> UpdateAccountAsync(string Id, User user)
        {
            try
            {
                var result = await _js.InvokeAsync<UserResult>("firestoreFunctions.updateAccount", Id, user);
                return result;
            }
            catch (JSException ex)
            {
                return new UserResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new UserResult { Success = false, Error = ex.Message };
            }
            
        }

        //Disable Account for logging in
        public async Task DisableAccountAsync(string docId)
        {
            await _js.InvokeVoidAsync("firestoreFunctions.disableAccount", docId);
        }

        //Enable Account for logging in
        public async Task EnableAccountAsync(string docId)
        {
            await _js.InvokeVoidAsync("firestoreFunctions.enableAccount", docId);
        }


        //Get User By Id
        public async Task<UserResult> GetUserById(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<UserResult>("firestoreFunctions.getAccountById", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new UserResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new UserResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class UserResult
    {
        public bool Success { get; set; }
        public List<User> Data { get; set; }
        public string Error { get; set; }
        public User User { get; set; }
    }
}
