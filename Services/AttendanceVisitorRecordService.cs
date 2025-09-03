using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class AttendanceVisitorRecordService
    {
        private readonly IJSRuntime _js;

        public AttendanceVisitorRecordService(IJSRuntime js)
        {
            _js = js;
        }

        //================================================Function=====================================

        //Add attendance member record
        public async Task<AttendanceMemberRecordResult> AddAttendanceVisitorRecordAsync(AttendanceVisitor att)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceMemberRecordResult>("firestoreFunctions.addAttendanceVisitorRecord", att);
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
        public async Task<AttendanceVisitorRecordResult> RemoveAttendanceVisitorAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceVisitorRecordResult>("firestoreFunctions.removeAttendanceVisitor", Id);
                return result;
            }
            catch(JSException jsEx)
            {
                return new AttendanceVisitorRecordResult { Success = false, Error = jsEx.Message };
            }
            catch(Exception ex)
            {
                return new AttendanceVisitorRecordResult { Success = false, Error = ex.Message };
            }
        }

        //Add attendance member record by attendance Id
        public async Task<AttendanceVisitorRecordResult> GetAttendanceVisitorByAttendanceIdAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceVisitorRecordResult>("firestoreFunctions.getAttendanceVisitorByAttendanceId", Id);
                return result;
            }
            catch (JSException jsEx)
            {
                return new AttendanceVisitorRecordResult
                {
                    Success = false,
                    Error = jsEx.Message
                };
            }
            catch (Exception ex)
            {
                return new AttendanceVisitorRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class AttendanceVisitorRecordResult
    {
        public bool Success { get; set; }
        public List<AttendanceVisitor> Data { get; set; }
        public string Error { get; set; }
    }
}
