using Microsoft.AspNetCore.Components;

namespace GospelReachCapstone.Services
{
    public class ProtectedPageBase : ComponentBase
    {
        [Inject] public AuthState Auth { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        private bool _hasChecked = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_hasChecked)
            {
                _hasChecked = true;

                if (!Auth.IsLoggedIn)
                {
                    Nav.NavigateTo("", forceLoad: true);
                }
            }
        }
    }
}
