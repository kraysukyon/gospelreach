using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class FinancialRecordService
    {
        private readonly IJSRuntime _js;
        public FinancialRecordService(IJSRuntime js)
        {
            _js = js;
        }
        //=========Functions============================

        //add financial record
        public async Task<FinancialRecordResult> AddFinancialRecordAsync(FinancialRecord record)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.addFinancialRecord", record);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get all financial record
        public async Task<FinancialRecordResult> GetAllFinancialRecordAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.getAllFinance");
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get all financial record
        public async Task<FinancialRecordResult> GetAllFinancialRecordByDepartmentAsync(string department)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.getAllFinanceByDepartment", department);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get financial datas by date range
        public async Task<FinancialRecordResult> GetFinancialDataByDateRange(DateOnly date1, DateOnly date2)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.getFinancialRecordByDateRange", date1, date2);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get financial datas by date range
        public async Task<FinancialRecordResult> GetDepartmentFinancialDataByDateRange(DateOnly date1, DateOnly date2, string department)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.getDepartmentFinancialRecordByDateRange", date1, date2, department);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get financial datas by date range
        public async Task<FinancialRecordResult> CheckReceiptNumber(string number)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.checkReceiptNumber", number);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Remove financial record
        public async Task<FinancialRecordResult> RemoveFinancialRecordAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.removeFinanceMens", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Get financial record by Id
        public async Task<FinancialRecordResult> GetFinancialRecordByIdAsync (string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.getFinanceMensById", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Update financial record by Id
        public async Task<FinancialRecordResult> UpdateFinancialRecordAsync(string Id, FinancialRecord mens)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialRecordResult>("firestoreFunctions.updateFinanceMensRecord", Id, mens);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialRecordResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class FinancialRecordResult
    {
        public bool Success { get; set; }
        public List<FinancialRecord> Data { get; set; }
        public FinancialRecord FinanceMens { get; set; }
        public string Error { get; set; }
    }
}
