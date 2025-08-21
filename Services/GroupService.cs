using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class GroupService
    {
        private readonly IJSRuntime _js;
        public GroupService(IJSRuntime jSRuntime)
        {
            _js = jSRuntime;
        }

        //===============Functions=================

        //Get
        public async Task<GroupResult> GetGroupsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<GroupResult>("firestoreFunctions.getGroup");
                return result;
            }
            catch (JSException ex)
            {
                return new GroupResult { Success = false, Error = ex.Message };
            }
        }

        //Get Group by Id
        public async Task<GroupResult> GetGroupByIdAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<GroupResult>("firestoreFunctions.getGroupById", Id);
                return result;
            }
            catch (JSException ex)
            {
                return new GroupResult { Success = false, Error = ex.Message };
            }
        }

        //Add
        public async Task<GroupResult> AddGroupAsync(Group group)
        {
            try
            {
                var result = await _js.InvokeAsync<GroupResult>("firestoreFunctions.addGroup", group);
                return result;
            }
            catch (JSException ex)
            {
                return new GroupResult { Success = false, Error = ex.Message };
            }
        }

        //Update Group
        public async Task<GroupResult> UpdateGroupAsync(Group group)
        {
            try
            {
                var result = await _js.InvokeAsync<GroupResult>("firestoreFunctions.updateGroup",group);
                return result;
            }
            catch (JSException ex)
            {
                return new GroupResult { Success = false, Error = ex.Message };
            }
        }

    }

    public class GroupResult
    {
        public bool Success { get; set; }
        public string Id { get; set; }
        public List<Group> Data { get; set; }
        public string Error { get; set; }
        public Group Group { get; set; }
    }
}
