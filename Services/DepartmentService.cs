using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class DepartmentService
    {
        private readonly IJSRuntime _js;

        public DepartmentService(IJSRuntime js)
        {
            _js = js;
        }

        //======================================================Function=====================================================//
        public async Task<DepartmentResult> GetDepartmentsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<DepartmentResult>("firestoreFunctions.getDepartment");
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
                var result = await _js.InvokeAsync<DepartmentResult>("firestoreFunctions.addDepartment", department);
                return result;
            }
            catch (JSException ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
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
                var result = await _js.InvokeAsync<DepartmentResult>("firestoreFunctions.getDepartmentById", docId);
                return result;
            }
            catch (JSException ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
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
                var result = await _js.InvokeAsync<DepartmentResult>("firestoreFunctions.updateDepartment", docId, department);
                return result;
            }
            catch (JSException ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
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
                var result = await _js.InvokeAsync<DepartmentResult>("firestoreFunctions.removeDepartment", docId);
                return result;
            }
            catch (JSException ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class DepartmentResult
    {
        public bool Success { get; set; }
        public List<Department> Data { get; set; }
        public string Error { get; set; }
    }
}
