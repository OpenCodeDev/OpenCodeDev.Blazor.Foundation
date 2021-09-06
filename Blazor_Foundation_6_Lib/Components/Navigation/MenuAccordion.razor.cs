using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions;


namespace OpenCodeDev.Blazor.Foundation.Components.Navigation
{
    public partial class MenuAccordion : ComponentBase, IDisposable
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
        /// True when menu direction is vertical. (Default: False)
        /// </summary>
        [Parameter]
        public bool Vertical { get; set; } = false;

        /// <summary>
        /// When true, all the menu will be toggle on! (AutoManaged must be true)
        /// </summary>
        [Parameter]
        public bool AutoOpen { get; set; } = false;


        /// <summary>
        /// Can include any option available.
        /// <br/><br/>
        /// <see href="https://get.foundation/sites/docs/accordion-menu.html#javascript-reference">Accordion</see>
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }

        /// <summary>
        /// True: Will Trigger Foundation Script Registration Automatically Without any Options.
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnInitialized()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid().HTMLCompliant().ToString();
            }
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
                    await JSRuntime.InvokeVoidAsync("AccordionMenuRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                    if (AutoOpen)
                    {
                        await JSRuntime.InvokeVoidAsync("ElementShowAll", Id);
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
