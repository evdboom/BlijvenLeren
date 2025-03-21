using Microsoft.AspNetCore.Components;

namespace BlijvenLerenWeb.Shared;

public partial class RedirectToLogin
{
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        Navigation.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(Navigation.Uri)}");
    }
}