using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class SubDepartmentService
    {
        private readonly IJSRuntime _js;

        public SubDepartmentService(IJSRuntime js)
        {
            _js = js;
        }

        //====================Function=======================//

        public async Task<SubDepartmentResult> GetSubDepartmentsAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<SubDepartmentResult>("firestoreFunctions.getSubDepartment", Id);
                return result;

            }
            catch (JSException jsEx)
            {
                return new SubDepartmentResult { Success = false, Error = jsEx.Message };
            }
            catch (Exception ex)
            {
                return new SubDepartmentResult { Success = false, Error = ex.Message };
            }
        }


    }

    public class SubDepartmentResult
    {
        public bool Success { get; set; }
        public List<SubDepartment> Data { get; set; }
        public string Error { get; set; }
    }
}
