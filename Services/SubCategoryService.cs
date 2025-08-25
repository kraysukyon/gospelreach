using GospelReachCapstone.Models;
using Microsoft.JSInterop;

namespace GospelReachCapstone.Services
{
    public class SubCategoryService
    {
        IJSRuntime _js;
        public SubCategoryService(IJSRuntime js)
        {
            _js = js;
        }

        //==============Functions=================

        //Get SubCategories
        public async Task<SubCategoryResult> GetSubCategories()
        {
            try
            {
                var result = await _js.InvokeAsync<SubCategoryResult>("firestoreFunctions.getSubCategory");
                return result;
            }
            catch (JSException jsEx)
            {
                return new SubCategoryResult { Success = false, Error = jsEx.Message };
            }
            catch (Exception ex)
            {
                return new SubCategoryResult { Success = false, Error = ex.Message };
            }
        }


    }

    public class SubCategoryResult
    {
        public bool Success { get; set; }
        public List<SubCategory> Data { get; set; }
        public string Error { get; set; }
    }
}
