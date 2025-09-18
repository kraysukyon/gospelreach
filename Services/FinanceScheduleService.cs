using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class FinanceScheduleService
    {
        private readonly IJSRuntime _js;
        public FinanceScheduleService(IJSRuntime js)
        {
            _js = js;
        }

        //Add Finance Event
        public async Task<FinanceScheduleResult> AddFinanceEventsAsync(FinanceSchedule financeEvents)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceScheduleResult>("firestoreFunctions.addFinanceEvent", financeEvents);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
        }

        //Remove all finance events that matchs the schedule Id
        public async Task<FinanceScheduleResult> RemoveFinanceRecordsByScheduleIdAsync(string schedId)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceScheduleResult>("firestoreFunctions.removeFinanceEventsByScheduleId", schedId);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
        }

        //Get documents that contains departmentId and scheduleId all
        public async Task<FinanceScheduleResult> GetAllFinanceEventsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceScheduleResult>("firestoreFunctions.getAllFinanceEvents");
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
        }

        //Get documents that contains departmentId and scheduleId all
        public async Task<FinanceScheduleResult> GetFinanceEventsByDepartmentIdAsync(string departmentName)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceScheduleResult>("firestoreFunctions.getFinanceEventsByDepartment", departmentName);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
        }

        //Update finance status
        public async Task<FinanceScheduleResult> UpdateFinanceEventStatus(string Id, bool isComplete)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceScheduleResult>("firestoreFunctions.updateFinanceEvent", Id, isComplete);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceScheduleResult { Success = false, Error = ex.Message };
            }
        }

        public class FinanceScheduleResult
        {
            public bool Success { get; set; }
            public List<FinanceSchedule> Data { get; set; }
            public string Error { get; set; }
        }
    }
}   
