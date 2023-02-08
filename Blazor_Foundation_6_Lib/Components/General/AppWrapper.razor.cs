using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.General
{
    public partial class AppWrapper : ComponentBase, IDisposable
    {
        public NovelReveal NovelRevealInstance { get; set; }

        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        public IStyleManagement? StyleManager { get; set; }
        public INovelRevealController? NovelRevealController { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public bool LegacyRevealController { get; set; } = true;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) {
                StyleManager = ServiceProvider.GetService<IStyleManagement>();
                NovelRevealController = ServiceProvider.GetService<INovelRevealController>();
                JSRuntime = ServiceProvider.GetService<IJSRuntime>();
                if (NovelRevealController != null)
                {
                    NovelRevealController.NovelRevealCtrlDotNet = DotNetObjectReference.Create(NovelRevealInstance);
                    NovelRevealController.OnStateChanged = () => NovelRevealInstance.StateChanged();
                }
                StateHasChanged();
            }
        }
        public void Dispose()
        {
            
        }
    }
}
