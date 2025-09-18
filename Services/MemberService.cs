using GospelReachCapstone.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace GospelReachCapstone.Services
{
    public class MemberService
    {
        private readonly IJSRuntime _js;
        public MemberService(IJSRuntime js)
        {
            _js = js;
        }

        //================================== Member Functions Section=============================================//

        public async Task<MembersResult> GetMembersAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<MembersResult>("firestoreFunctions.getMembers");
                return result;
            }
            catch (JSException ex)
            {
                return new MembersResult
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<MembersResult> AddMemberAsync(Member member)
        {
            try
            {
                var result = await _js.InvokeAsync<MembersResult>("firestoreFunctions.addMember", member);
                return result;
            }
            catch (JSException ex)
            {
                return new MembersResult { Success = false, Error = ex.Message};
            }
            catch (Exception ex)
            {
                return new MembersResult { Success = false, Error = ex.Message };
            }
        }

        public async Task updateMemberAsync(string memberId, Member member)
        {
            try
            {
                await _js.InvokeVoidAsync("firestoreFunctions.updateMember", memberId, member);
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        public async Task deleteMemberAsync(string memberId)
        {
            try
            {
                await _js.InvokeVoidAsync("firestoreFunctions.deleteMember", memberId);
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }

    public class MembersResult
    {
        public bool Success { get; set; }
        public List<Member> Data { get; set; }
        public string Error { get; set; }
    }
}
