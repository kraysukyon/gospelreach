using GospelReachCapstone.Models;
using Microsoft.JSInterop;
using System.Text.Json;

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

        //Getting the Attendance List
        public async Task<List<Attendance>> GetAttendanceAsync()
        {
            var result =  await _jsRuntime.InvokeAsync<Attendance[]>("firestoreFunctions.getAttendance");
            return result.ToList();
        }

        
        //Add Attendance
        public async Task<(bool Success, string Message)> AddAttendanceAsync(Attendance attendance)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<AttendanceResult>("firestoreFunctions.addAttendance", attendance);
                return result.Success ? (true, "Attendance added successfully") : (false, result.Error ?? "Failed to add attendance");
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Error adding attendance: {ex.Message}");
                return (false, "Error adding attendance");
            }

        }

        public async Task UpdateAttendanceAsync(string docId, Attendance attendance)
        {
            await _jsRuntime.InvokeVoidAsync("firestoreFunctions.editAttendance", docId, attendance);
        }
        
        public async Task DeleteAttendanceAsync(string docId)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.deleteAttendance", docId);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Error deleting attendancesssss: {ex.Message}");
            }
        }

        public async Task<List<Member>> GetMembersAsync()
        {
            try
            {
                var memberList =  await _jsRuntime.InvokeAsync<Member[]>("firestoreFunctions.getMembers");
                return memberList.ToList();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Error fetching members: {ex.Message}");
                return new List<Member>();
            }
        }

        public async Task<bool> AddMemberAsync(Member member)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<JsonElement>("firestoreFunctions.addMember", member);
                await _jsRuntime.InvokeVoidAsync("alert", "Member Successfuly added!");
                return result.GetProperty("success").GetBoolean();
            }
            catch
            {
                return false;
            }
        }

        //=========================Events Section========================================//
        public async Task<List<Event>> getEventsAsync()
        {
            try
            {
                var events = await _jsRuntime.InvokeAsync<Event[]>("firestoreFunctions.getEvents");
                return events.ToList();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", "Failed to fetch list:" + ex.Message);
                return new List<Event>();
            }
        }

        public async Task<bool> addEventAsync(Event events)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<JsonElement>("firestoreFunctions.addEvent", events);
                await _jsRuntime.InvokeVoidAsync("alert", "Event Successfuly added!");
                return result.GetProperty("success").GetBoolean();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", "Failed to add event: " + ex.Message);
                return false;
            }
            
        }

        public async Task editEventAsync(string eventId,Event events)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.editEvent", eventId, events);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        public async Task DeleteEventAsync(string Id)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.deleteEvent", Id);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                //throw new Exception("Failed to delete document from Firestore", ex);
            }
        }

    }

    public class AttendanceResult
    {
        public bool Success { get; set; }
        public string Id { get; set; }
        public string Error { get; set; }
    }
}
