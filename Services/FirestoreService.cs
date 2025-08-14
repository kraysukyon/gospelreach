using GospelReachCapstone.Models;
using Microsoft.AspNetCore.Components;
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
        public async Task<List<User>> GetAccountAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<User[]>("firestoreFunctions.getAccounts");
                return result.ToList();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", $"Error fetching accounts: {ex.Message}");
                return new List<User>();
            }

        }


        //Update Accounts
        public async Task UpdateAccountAsync(string docId, string role)
        {
            await _jsRuntime.InvokeVoidAsync("firestoreFunctions.updateAccount", docId, role);
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
        public async Task<AttendanceResult> GetAttendanceAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<AttendanceResult>("firestoreFunctions.getAttendance");
                return result;
            }
            catch (JSException ex)
            {
                return new AttendanceResult { Success = false, Error = ex.Message };
            }
        }


        //Add Attendance
        public async Task<AttendanceResult> AddAttendanceAsync(Attendance attendance)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<AttendanceResult>("firestoreFunctions.addAttendance", attendance);
                return result;
            }
            catch (JSException ex)
            {
                return new AttendanceResult { Success = false, Error = ex.Message };
            }

        }

        public async Task<AttendanceResult> UpdateAttendanceAsync(string docId, Attendance attendance)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<AttendanceResult>("firestoreFunctions.editAttendance", docId, attendance);
                return result;
            }
            catch (JSException ex)
            {
                return new AttendanceResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<AttendanceResult> DeleteAttendanceAsync(string docId)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<AttendanceResult>("firestoreFunctions.deleteAttendance", docId);
                return result;
            }
            catch (JSException ex)
            {
                return new AttendanceResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //================================== Member Section=============================================//

        public async Task<MembersResult> GetMembersAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<MembersResult>("firestoreFunctions.getMembers");
                return result;
            }
            catch (JSException ex)
            {
                return new MembersResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<bool> AddMemberAsync(Member member)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<JsonElement>("firestoreFunctions.addMember", member);
                return result.GetProperty("success").GetBoolean();
            }
            catch
            {
                return false;
            }
        }

        public async Task updateMemberAsync(string memberId, Member member)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.updateMember", memberId, member);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        public async Task deleteMemberAsync(string memberId)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.deleteMember", memberId);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        //======================================================Events Section========================================//
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

        public async Task editEventAsync(string eventId, Event events)
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

        //======================================================Music Section===================================================//
        public async Task<List<Song>> GetSongsAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<Song[]>("firestoreFunctions.getSongs");
                return result.ToList();

            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new List<Song>();
            }
        }

        public async Task<bool> AddSongAsync(Song song)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<JsonElement>("firestoreFunctions.addSong", song);
                return result.GetProperty("success").GetBoolean();
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return false;
            }
        }
        public async Task UpdateSongAsync(string Id, Song song)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.updateSong", Id, song);
            } catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        public async Task DeleteSongAsync(string Id)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.deleteSong", Id);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        //Resize textarea of input and output
        public async Task ResizeTextArea(ElementReference input, ElementReference output)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firestoreFunctions.resizeTextarea", input, output);
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
            }
        }

        //======================================================Department Section=====================================================//
        public async Task<DepartmentResult> GetDepartmentsAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentResult>("firestoreFunctions.getDepartment");
                return result;
            }
            catch (JSException ex)
            {
                return new DepartmentResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentResult> AddDepartmentAsync(Department department)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentResult>("firestoreFunctions.addDepartment", department);
                return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentResult> GetDepartmentByIdAsync(string docId)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentResult>("firestoreFunctions.getDepartmentById", docId);
                return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }


        public async Task<DepartmentResult> UpdateDepartmentAsync(string docId, Department department)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentResult>("firestoreFunctions.updateDepartment", docId, department);
                return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentResult> RemoveDepartmentAsync(string docId)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentResult>("firestoreFunctions.removeDepartment", docId);
                return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //==================================Department Member Section=============================================//
        public async Task<DepartmentMemberResult> GetDepartmentMemberAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.getDepartmentMembers");
                return result;
            }
            catch (JSException ex)
            {
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentMemberResult> AddDepartmentMemberAsync(DepartmentMember member)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.addDepartmentMember", member);
                return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentMemberResult> UpdateDepartmentMemberAsync(string Id, DepartmentMember dep)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.updateDepartmmentMember", Id, dep);
                return result;
            }
            catch(JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentMemberResult> RemoveDepartmentMemberAsync(string Id)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.removeDepartmentmember", Id);
                    return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //==================================Department Member Section=============================================//

        //add schedule
        public async Task<ScheduleResult> AddScheduleAsync(Schedule sched)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<ScheduleResult>("firestoreFunctions.addSchedule", sched);
                return result;
            }
            catch (JSException ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Fetch Data for schedule
        public async Task<ScheduleResult> GetScheduleAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<ScheduleResult>("firestoreFunctions.getSchedule");
                return result;
            }
            catch (JSException ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Update Schedule
        public async Task<ScheduleResult> UpdateScheduleAsync(string Id, Schedule sched)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<ScheduleResult>("firestoreFunctions.updateSchedule", Id, sched);
                return result;
            }
            catch (JSException ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Remove Schedule
        public async Task<ScheduleResult> RemoveScheduleAsync(string Id)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<ScheduleResult>("firestoreFunctions.removeSchedule", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class AttendanceResult
    {
        public bool Success { get; set; }
        public string Id { get; set; }
        public string Error { get; set; }
    }

    public class MembersResult
    {
        public bool Success { get; set; }
        public List<Member> Data { get; set; }
        public string Error { get; set; }
    }

    public class DepartmentResult
    {
        public bool Success { get; set; }
        public List<Department> Data { get; set; }
        public string Error { get; set; }
    }

    public class DepartmentMemberResult
    {
        public bool Success { get; set; }
        public List<DepartmentMember> Data { get; set; }
        public string Error { get; set; }
    }

    public class ScheduleResult
    {
        public bool Success { get; set; }
        public List<Schedule> Data { get; set; }
        public string Error { get; set; }
    }
}
