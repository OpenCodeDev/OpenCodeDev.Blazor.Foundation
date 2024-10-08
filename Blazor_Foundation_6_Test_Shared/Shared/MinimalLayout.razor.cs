using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Containers;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Shared
{
    public partial class MinimalLayout : LayoutComponentBase
    {

        private OffCanvas OffCanvasBottom;
        private async Task TriggerButton()
        {
            await OffCanvasBottom.TriggerToggle();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StyleManager.OnUpdate += OnStyleChanged;
            // If --top-bar-bg-color already exist, it will be updated.
            // Note: If description is available it will be preserve
            // true/false trigger update
            StyleManager.Set("--top-bar-bg-color", "#1c2463", "Top bar background color.", true);

        }

        public async Task OnStyleChanged()
        {
            await InvokeAsync(() => StateHasChanged());
        }

        public void Dispose()
        {
            StyleManager.OnUpdate -= OnStyleChanged;
        }

    }
}
