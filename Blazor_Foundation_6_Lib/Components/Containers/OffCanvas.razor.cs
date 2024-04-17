using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Extensions;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal;

namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class OffCanvas : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().HTMLCompliant().ToString();

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// position-left, position-right, position-top or position-bottom
        /// </summary>
        [Parameter]
        public string Position { get; set; } = "position-left";

        /// <summary>
        /// overlap or push (default)
        /// </summary>
        [Parameter]
        public string DataTransition { get; set; } = "overlap";

        /// <summary>
        /// Time to Open/Close: .75s, 100ms ... 500ms (default)
        /// </summary>
        [Parameter]
        public string DataTransitionTime { get; set; } = "500ms";

        /// <summary>
        /// True: Off-Canvas is Absolute Position
        /// </summary>
        [Parameter]
        public bool Absolute { get; set; } = false;

        /// <summary>
        /// True: when click on canvas content then off canvas  (Default: True)
        /// </summary>
        [Parameter]
        public bool CloseOnClick { get; set; } = true;

        /// <summary>
        /// True: tint overlay over the canvas content. (Default: True)
        /// </summary>
        [Parameter]
        public bool ContentOverlay { get; set; } = true;

        /// <summary>
        /// If false, you will have to create initializer for this Foundation 6 component yourself in javascript. (Default: True)
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;

        /// <summary>
        /// List of Options to pass when initilization is handled by Blazor. Leave blank if AutoManaged = false.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }

        /// <summary>
        /// Set true to open automatically on start. (AutoManaged must be = true)
        /// </summary>
        [Parameter]
        public bool AutoOpen { get; set; } = false;

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }
        
        /// <summary>
        /// Called when closing event starts
        /// </summary>
        [Parameter] public EventCallback OnClosing { get; set; }

        /// <summary>
        /// Called when closed event called.
        /// </summary>
        [Parameter] public EventCallback OnClosed { get; set; }

        /// <summary>
        /// Called when opening starts
        /// </summary>
        [Parameter] public EventCallback OnOpening { get; set; }

        /// <summary>
        /// Called when canvas is opened.
        /// </summary>
        [Parameter] public EventCallback OnOpened { get; set; }

        [Inject] IJSRuntime JS { get; set; }
        private DotNetObjectReference<OffCanvas> CtrlDotNet { get; set; }

        protected override void OnInitialized()
        {
            CtrlDotNet = DotNetObjectReference.Create(this);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) {
                if (AutoManaged) {
                    await JSRuntime.InvokeVoidAsync("OffCanvasRegister", CtrlDotNet, Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                    if (AutoOpen) {
                        await TriggerOpen();
                    }
                }
            }

        }

        /// <summary>
        /// Called by JS when event triggered
        /// </summary>
        public async Task OnFoundationOpening() => await OnOpening.InvokeAsync();

        /// <summary>
        /// Called by JS when event triggered
        /// </summary>
        public async Task OnFoundationOpened() => await OnOpened.InvokeAsync();

        /// <summary>
        /// Called by JS when event triggered
        /// </summary>
        public async Task OnFoundationClosing() => await OnClosing.InvokeAsync();

        /// <summary>
        /// Called by JS when event triggered
        /// </summary>
        public async Task OnFoundationClosed() => await OnClosed.InvokeAsync();


        public void Dispose()
        {
            if (AutoManaged)
            {
                JSRuntime.InvokeVoidAsync("FoundationDestroy", Id);
            }
        }

        /// <summary>
        /// Toggle Panel On/Off (Warning: Might Break Grid Cell (Maybe Blazor Issue?)
        /// </summary>
        public async Task TriggerToggle()
        {
            await JSRuntime.InvokeVoidAsync("ElementToggle", Id);
        }

        /// <summary>
        /// Toggle Panel On (Warning: Might Break Grid Cell (Maybe Blazor Issue?)
        /// </summary>
        public async Task TriggerOpen()
        {
            await JSRuntime.InvokeVoidAsync("ElementOpen", Id);
        }

        /// <summary>
        /// Toggle Panel Off (Warning: Might Break Grid Cell (Maybe Blazor Issue?)
        /// </summary>
        public async Task TriggerClose()
        {
            await JSRuntime.InvokeVoidAsync("ElementClose", Id);
        }
    }
}
