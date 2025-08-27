using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class VisitorService
    {
        private readonly IJSRuntime _js;
        public VisitorService(IJSRuntime js)
        {
            _js = js;
        }

        //======================Functions============================
        public async Task<VisitorResult> GetVisitorsAsync()
        {
            try
            {
                var result = await _js.InvokeAsync<VisitorResult>("firestoreFunctions.getVisitors");
                return result;
            }
            catch(JSException jsEx)
            {
                return new VisitorResult { Success = false, Error = jsEx.Message };
            }
            catch (Exception ex)
            {
                return new VisitorResult { Success = false, Error = ex.Message };
            }
        }

        public async Task<VisitorResult> AddVisitorAsync(Visitor visitor)
        {
            try
            {
                var result = await _js.InvokeAsync<VisitorResult>("firestoreFunctions.addVisitor", visitor);
                return result;
            }
            catch (JSException jsEx)
            {
                return new VisitorResult { Success = false, Error = jsEx.Message };
            }
            catch (Exception ex)
            {
                return new VisitorResult { Success = false, Error = ex.Message };
            }
        }

    }

    public class VisitorResult()
    {
        public bool Success { get; set; }
        public List<Visitor> Data { get; set; }
        public string Error { get; set; }
    }
}
