using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class FinanceEventsService
    {
        private readonly IJSRuntime _js;
        public FinanceEventsService(IJSRuntime js)
        {
            _js = js;
        }

        //Add Finance Event
        public async Task<FinanceEventResult> AddFinanceEventsAsync(FinanceEvents financeEvents)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceEventResult>("firestoreFunctions.addFinanceEvent", financeEvents);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
        }

        //Remove all finance events that matchs the schedule Id
        public async Task<FinanceEventResult> RemoveFinanceRecordsByScheduleIdAsync(string schedId)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceEventResult>("firestoreFunctions.removeFinanceEventsByScheduleId", schedId);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
        }

        //Get documents that contains departmentId and scheduleId all
        public async Task<FinanceEventResult> GetAllFinanceEventsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceEventResult>("firestoreFunctions.getAllFinanceEvents");
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
        }

        //Get documents that contains departmentId and scheduleId all
        public async Task<FinanceEventResult> GetFinanceEventsByDepartmentIdAsync(string departmentName)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceEventResult>("firestoreFunctions.getFinanceEventsByDepartment", departmentName);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
        }

        //Update finance status
        public async Task<FinanceEventResult> UpdateFinanceEventStatus(string Id, bool isComplete)
        {
            try
            {
                var result = await _js.InvokeAsync<FinanceEventResult>("firestoreFunctions.updateFinanceEvent", Id, isComplete);
                return result;
            }
            catch (JSException ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new FinanceEventResult { Success = false, Error = ex.Message };
            }
        }

        public class FinanceEventResult
        {
            public bool Success { get; set; }
            public List<FinanceEvents> Data { get; set; }
            public string Error { get; set; }
        }
    }
}   
