using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Extensions.Util;
using Microsoft.JSInterop;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Sticky
{
    public partial class StickyContainer : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Add Custom Class
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// Define Unique ID for the Tag
        /// </summary>
        [Parameter]
        public string Id { get; set; }



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

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AutoManaged)
                {
                    await JSRuntime.InvokeVoidAsync("StickyRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                }
            }

        }

        protected override void OnInitialized()
        {


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
