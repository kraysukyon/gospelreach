using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class AccountsService
    {
        private readonly IJSRuntime _js;
        public AccountsService(IJSRuntime js)
        {
            _js = js;
        }

        //====================Functions======================

        public async Task<AccountResults> GetUserAccountsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<AccountResults>("firestoreFunctions.getAccounts");
                return result;
            }
            catch (JSException ex)
            {
                return new AccountResults
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new AccountResults
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
        public async Task UpdateAccountAsync(string docId, string role)
        {
            await _js.InvokeVoidAsync("firestoreFunctions.updateAccount", docId, role);
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

    }

    public class AccountResults
    {
        public bool Success { get; set; }
        public List<User> Data { get; set; }
        public string Error { get; set; }
    }
}
