using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class FinancialMensService
    {
        private readonly IJSRuntime _js;
        public FinancialMensService(IJSRuntime js)
        {
            _js = js;
        }
        //=========Functions============================

        //add financial record
        public async Task<FinancialMensResult> AddFinancialMensRecordAsync(FinancialMensRecord record)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.addFinanceMens", record);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get all financial record
        public async Task<FinancialMensResult> GetAllFinancialRecordAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.getAllFinance");
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get financial datas by date range
        public async Task<FinancialMensResult> GetFinancialDataByDateRange(DateOnly date1, DateOnly date2)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.getFinancialRecordByDateRange", date1, date2);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //get financial datas by date range
        public async Task<FinancialMensResult> CheckReceiptNumber(string number)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.checkReceiptNumber", number);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Remove financial record
        public async Task<FinancialMensResult> RemoveFinancialRecordAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.removeFinanceMens", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Get financial record by Id
        public async Task<FinancialMensResult> GetFinancialRecordByIdAsync (string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.getFinanceMensById", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Update financial record by Id
        public async Task<FinancialMensResult> UpdateFinancialRecordAsync(string Id, FinancialMensRecord mens)
        {
            try
            {
                var result = await _js.InvokeAsync<FinancialMensResult>("firestoreFunctions.updateFinanceMensRecord", Id, mens);
                return result;
            }
            catch (JSException ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new FinancialMensResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class FinancialMensResult
    {
        public bool Success { get; set; }
        public List<FinancialMensRecord> Data { get; set; }
        public FinancialMensRecord FinanceMens { get; set; }
        public string Error { get; set; }
    }
}
