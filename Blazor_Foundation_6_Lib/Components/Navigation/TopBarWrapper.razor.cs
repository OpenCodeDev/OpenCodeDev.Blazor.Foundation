using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions.Util;


namespace OpenCodeDev.Blazor.Foundation.Components.Navigation
{
    public partial class TopBarWrapper : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class (eg. "menu custom").
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Inline CSS
        /// </summary>
        [Parameter]
        public string WrapperStyle { get; set; }

        /// <summary>
        /// Inline CSS
        /// </summary>
        [Parameter]
        public string TitleBarStyle { get; set; }

        /// <summary>
        /// True when stick top bar (uses Plugin Sticky). (Default: False)
        /// </summary>
        [Parameter]
        public bool Sticky { get; set; }

        /// <summary>
        /// True: Will Trigger Foundation Script Registration Automatically Without any Options.
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;

        /// <summary>
        /// List of Options to pass when initilization is handled by Blazor. Leave blank if AutoManaged = false.
        /// <br/><br/><see href="https://get.foundation/sites/docs/sticky.html">Sticky Options</see>
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }


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
                if (AutoManaged && Sticky)
                {
                    await JSRuntime.InvokeVoidAsync("StickyRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
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
    }
}
