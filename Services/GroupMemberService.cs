using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class GroupMemberService
    {
        private readonly IJSRuntime _js;
        public GroupMemberService(IJSRuntime js)
        {
            _js = js;
        }

        //=========Functions============================

        //Fetching GroupMember
        public async Task<GroupMemberResult> GetGroupMembersByGroupIdAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<GroupMemberResult>("firestoreFunctions.getGroupMembersByGroupId", Id);
                return result;

            }
            catch (JSException ex)
            {
                return new GroupMemberResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        //Add group members
        public async Task<GroupMemberResult> AddGroupMembersAsync(GroupMember gm)
        {
            try
            {
                var result = await _js.InvokeAsync<GroupMemberResult>("firestoreFunctions.addGroupmembers", gm);
                return result;
            }
            catch (JSException ex)
            {
                return new GroupMemberResult { Success = false, Error = ex.Message };
            }
        }

        //Remove All Group Members
        public async Task<GroupMemberResult> RemoveGroupMembersAsync(string Id)
        {
            try
            {
                var result = await _js.InvokeAsync<GroupMemberResult>("firestoreFunctions.removeGroupMembers", Id);
                return result;
            }
            catch(JSException ex)
            {
                return new GroupMemberResult { Success = false, Error = ex.Message };
            }
        }
    }

    public class GroupMemberResult
    {
        public bool Success { get; set; }
        public List<GroupMember> Data { get; set; }
        public string Error { get; set; }
    }
}
