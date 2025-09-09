using GospelReachCapstone.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class ScheduleService
    {
        private readonly IJSRuntime _js;
        public ScheduleService(IJSRuntime js)
        {
            _js = js;
        }

        //=========Functions============================
        //add schedule
        public async Task<ScheduleResult> AddScheduleAsync(Schedule sched)
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.addSchedule", sched);
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
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getSchedule");
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
        public async Task<ScheduleResult> GetScheduleByDateAsync(int month, int year)
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getScheduleByDate", month, year);
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

        //Fetch schedules by status
        public async Task<ScheduleResult> GetUpcomingSchedules ()
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getUpcomingSchedules");
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

        public async Task<ScheduleResult> GetOngoingSchedules()
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getOngoingSchedules");
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

        public async Task<ScheduleResult> GetCompletedSchedules()
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getCompletedSchedules");
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

        public async Task<ScheduleResult> GetSchedulesWithMissingFinanceByDepartment(string DepartmentName)
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getSchedulesWithMissingFinancialRecords", DepartmentName);
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
            catch (Exception ex)
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
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.updateSchedule", Id, sched);
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
            catch(Exception ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Update Schedule
        public async Task<ScheduleResult> UpdateScheduleAttendance(string Id, bool hasAttendance)
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.updateScheduleAttendance", Id, hasAttendance);
                return result;
            }
            
            catch (JSException jsEx)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = jsEx.Message
                };
            }
            catch (Exception ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ScheduleResult> UpdateScheduleFinanceById(string Id, bool hasFinance)
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.updateScheduleFinance", Id, hasFinance);
                return result;
            }

            catch (JSException jsEx)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = jsEx.Message
                };
            }
            catch (Exception ex)
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
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.removeSchedule", Id);
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

        //Get Schedule By Id
        public async Task<ScheduleResult> GetScheduleByIdAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<ScheduleResult>("firestoreFunctions.getScheduleById", Id);
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
            catch (Exception ex)
            {
                return new ScheduleResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Resize textarea of input and output
        public async Task ResizeDetails(ElementReference input)
        {
            try
            {
                await _js.InvokeVoidAsync("firestoreFunctions.textareaResize", input);
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }

    public class ScheduleResult
    {
        public bool Success { get; set; }
        public List<Schedule> Data { get; set; }
        public string Error { get; set; }
        public Schedule Schedule { get; set; }
    }
}
