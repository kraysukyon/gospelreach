using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class FirestoreService
    {
        private readonly IJSRuntime _jsRuntime;

        public FirestoreService(IJSRuntime jsruntime)
        {
            _jsRuntime = jsruntime;
        }

        //Getting the Accounts List
        public async Task<List<Accounts>> GetAccountAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<Accounts[]>("firestoreFunctions.getAccounts");
                return result.ToList();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Error fetching accounts: {ex.Message}");
                return new List<Accounts>();
            }
            
        }

        //Update Accounts
        public async Task UpdateAccountAsync(string docId,Accounts accounts)
        {
            await _jsRuntime.InvokeVoidAsync("firestoreFunctions.updateAccount", docId, accounts);
        }

        //Disable Account for logging in
        public async Task DisableAccountAsync(string docId)
        {
            await _jsRuntime.InvokeVoidAsync("firestoreFunctions.disableAccount", docId);
        }

        //Enable Account for logging in
        public async Task EnableAccountAsync(string docId)
        {
            await _jsRuntime.InvokeVoidAsync("firestoreFunctions.enableAccount", docId);
        }
    }
}
