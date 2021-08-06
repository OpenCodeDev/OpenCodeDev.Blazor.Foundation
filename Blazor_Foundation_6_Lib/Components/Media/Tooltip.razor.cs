using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Extensions.Util;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class Tooltip : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Content appears inside the tooltip
        /// </summary>
        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// Define the position of tooltip <br/>
        /// top, right or left (Default: top)
        /// </summary>
        [Parameter]
        public string Position { get; set; } = "top";

        /// <summary>
        /// If False, tooltip will not stay open after click. (Default: True)
        /// </summary>
        [Parameter]
        public bool DataClickOpen { get; set; } = true;

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// True: Will Trigger Foundation Script Registration Automatically Without any Options.
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true; // Automatically Create the Canvas with Foundation JS Script.

        /// <summary>
        /// List of Options to pass when initilization is handled by Blazor. Leave blank if AutoManaged = false.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }


        protected override void OnInitialized()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid().HTMLCompliant().ToString(); ;
            }
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AutoManaged)
                {
                    //Console.WriteLine("Accordion Registering");
                    await JSRuntime.InvokeVoidAsync("TooltipRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                }
            }

        }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }
        public void Dispose()
        {
            if (AutoManaged)
            {
                JSRuntime.InvokeVoidAsync("FoundationDestroy", Id);
            }
        }

        public async Task TriggerToggle()
        {
            await JSRuntime.InvokeVoidAsync("ElementToggle", Id);
        }

        public async Task TriggerHide()
        {
            await JSRuntime.InvokeVoidAsync("ElementHide", Id);
        }

        public async Task TriggerShow()
        {
            await JSRuntime.InvokeVoidAsync("ElementShow", Id);
        }
    }
}
