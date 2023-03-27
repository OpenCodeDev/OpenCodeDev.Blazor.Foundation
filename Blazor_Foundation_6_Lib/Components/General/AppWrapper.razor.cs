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
        internal IServiceProvider ServiceProvider { get; set; }

        internal IStyleManagement? StyleManager { get; set; }
        internal INovelRevealController? NovelRevealController { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }



        /// <summary>
        /// Include Legacy Reveal Controller for backward compatibility (Default: true)
        /// </summary>
        [Parameter]
        public bool LegacyRevealController { get; set; } = true;

        protected override void OnInitialized()
        {
            StyleManager = ServiceProvider.GetService<IStyleManagement>();
            NovelRevealController = ServiceProvider.GetService<INovelRevealController>();
            if (StyleManager != null) 
                StyleManager.OnUpdate += StateChanged;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) {

                if (NovelRevealController != null)
                {
                    NovelRevealController.NovelRevealCtrlDotNet = DotNetObjectReference.Create(NovelRevealInstance);
                    NovelRevealController.OnStateChanged = () => NovelRevealInstance.StateChanged();
                    
                }
                StateHasChanged();
            }
        }

        public async Task StateChanged()
        {

        }
        public void Dispose()
        {
            if (StyleManager != null)
                StyleManager.OnUpdate += StateChanged;
        }
    }
}
