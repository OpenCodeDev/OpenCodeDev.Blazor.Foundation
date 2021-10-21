using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Extensions;
using Microsoft.JSInterop;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class OrbitWrapper: ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// If false, you will have to create initializer for this Foundation 6 component yourself in javascript. (Default: True)
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;

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
                    await JSRuntime.InvokeVoidAsync("OrbitRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
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
