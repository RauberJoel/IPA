using Microsoft.AspNetCore.Components;

namespace ScrumRetroApp.Blazor.Components
{
    public class HamburgerMenuComponentBase : ComponentBase
    {
        public bool ShowDropDown { get; set; }

        public void Visible()
        {
            ShowDropDown = !ShowDropDown;
            StateHasChanged();
        }

    }
}
