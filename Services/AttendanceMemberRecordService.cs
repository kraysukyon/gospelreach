using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class AttendanceMemberRecordService
    {
        private readonly IJSRuntime _js;

        public AttendanceMemberRecordService(IJSRuntime js)
        {
            _js = js;
        }

        //================================================Function=====================================

        //Add attendance member record
        public async Task<AttendanceMemberRecordResult> AddAttendanceMemberRecordAsync(AttendanceMemberRecord att)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceMemberRecordResult>("firestoreFunctions.addAttendanceMemberRecor", att);
                return result;
            }
            catch(JSException jsEx)
            {
                return new AttendanceMemberRecordResult
                {
                    Success = false,
                    Error = jsEx.Message
                };
            }
            catch (Exception ex)
            {
                return new AttendanceMemberRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Remove AttendanceMemberRecord
        public async Task<AttendanceMemberRecordResult> RemoveAttendanceRecordAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceMemberRecordResult>("firestoreFunctions.removeAttendanceMember", Id);
                return result;
            }
            catch(JSException jsEx)
            {
                return new AttendanceMemberRecordResult { Success = false, Error = jsEx.Message };
            }
            catch(Exception ex)
            {
                return new AttendanceMemberRecordResult { Success = false, Error = ex.Message };
            }
        }

        //Add attendance member record by attendance Id
        public async Task<AttendanceMemberRecordResult> GetAttendanceRecordByAttendanceId(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceMemberRecordResult>("firestoreFunctions.getAttendanceMemberByAttendanceId", Id);
                return result;
            }
            catch (JSException jsEx)
            {
                return new AttendanceMemberRecordResult
                {
                    Success = false,
                    Error = jsEx.Message
                };
            }
            catch (Exception ex)
            {
                return new AttendanceMemberRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class AttendanceMemberRecordResult
    {
        public bool Success { get; set; }
        public List<AttendanceMemberRecord> Data { get; set; }
        public string Error { get; set; }
    }
}
