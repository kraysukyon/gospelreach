using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class AttendanceService
    {
        private readonly IJSRuntime _js;
        public AttendanceService(IJSRuntime js)
        {
            _js = js;
        }

        //============Functions==================
        //Getting the Attendance List
        public async Task<AttendanceResult> GetAttendanceAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceResult>("firestoreFunctions.getAttendance");
                return result;
            }
            catch (JSException ex)
            {
                return new AttendanceResult { Success = false, Error = ex.Message };
            }
        }

        //Getting the Attendance List
        public async Task<AttendanceResult> GetAttendanceByDateAsync(int month, int year)
        {
            try
            {
                var result = await _js.InvokeAsync<AttendanceResult>("firestoreFunctions.getAttendanceByMonthYear", month, year);
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
                var result = await _js.InvokeAsync<AttendanceResult>("firestoreFunctions.addAttendance", attendance);
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
                var result = await _js.InvokeAsync<AttendanceResult>("firestoreFunctions.editAttendance", docId, attendance);
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
                var result = await _js.InvokeAsync<AttendanceResult>("firestoreFunctions.deleteAttendance", docId);
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


    }

    public class AttendanceResult
    {
        public bool Success { get; set; }
        public string Id { get; set; }
        public List<Attendance> Data { get; set; }
        public string Error { get; set; }
    }
}
