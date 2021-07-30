using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Extensions.Util;
using Microsoft.JSInterop;

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
        public string Id { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// position-left, position-right, position-top or position-bottom
        /// </summary>
        [Parameter]
        public string Position { get; set; }

        /// <summary>
        /// overlap or push
        /// </summary>
        [Parameter]
        public string DataTransition { get; set; }

        /// <summary>
        /// True: Off-Canvas is Absolute Position
        /// </summary>
        [Parameter]
        public bool Absolute { get; set; }

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

        protected override void OnInitialized()
        {

        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AutoManaged)
                {
                    await JSRuntime.InvokeVoidAsync("OffCanvasRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                    if (AutoOpen)
                    {
                        await TriggerOpen();
                    }

                }
            }

        }


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
