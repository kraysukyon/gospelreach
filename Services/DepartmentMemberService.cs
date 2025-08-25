using GospelReachCapstone.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace GospelReachCapstone.Services
{
    public class DepartmentMemberService
    {
        private readonly IJSRuntime _jsRuntime;

        public DepartmentMemberService(IJSRuntime jsruntime)
        {
            _jsRuntime = jsruntime;
        }

        //==================================Functions=============================================//
        public async Task<DepartmentMemberResult> GetDepartmentMemberAsync()
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.getDepartmentMembers");
                return result;
            }
            catch (JSException ex)
            {
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentMemberResult> AddDepartmentMemberAsync(DepartmentMember member)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.addDepartmentMember", member);
                return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentMemberResult> UpdateDepartmentMemberAsync(string Id, DepartmentMember dep)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.updateDepartmmentMember", Id, dep);
                return result;
            }
            catch(JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DepartmentMemberResult> RemoveDepartmentMemberAsync(string Id)
        {
            try
            {
                var result = await _jsRuntime.InvokeAsync<DepartmentMemberResult>("firestoreFunctions.removeDepartmentmember", Id);
                    return result;
            }
            catch (JSException ex)
            {
                await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                return new DepartmentMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }

    public class DepartmentMemberResult
    {
        public bool Success { get; set; }
        public List<DepartmentMember> Data { get; set; }
        public string Error { get; set; }
    }
}
