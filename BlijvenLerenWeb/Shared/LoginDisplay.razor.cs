using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlijvenLerenWeb.Shared;

public partial class LoginDisplay
{
    [Inject] 
    private NavigationManager Navigation { get; set; } = default!;

    private void BeginLogout(MouseEventArgs args)
    {
        Navigation.NavigateToLogout("authentication/logout");
    }
}