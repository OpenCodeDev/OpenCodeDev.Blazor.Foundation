using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class MenuSystemRight : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

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
        /// Link the parent reference MenuSystemWrapper
        /// </summary>
        [Parameter]
        public MenuSystemWrapper Wrapper { get; set; }

        /// <summary>
        /// Set Wrapper ID (Alternative to Direct Wrapper Link)
        /// </summary>
        [Parameter]
        public string WrapperId { get; set; } = null;

        protected override void OnInitialized()
        {

        }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }

        public async Task<bool> IsOpen()
        {
            if (Wrapper == null && WrapperId == null)
            {
                throw new Exception("MenuSystem requires wrapper to be linked if using open/close c# function.");
            }
            return await JSRuntime.InvokeAsync<bool>("MenuSystemIsOpen", Wrapper == null ? WrapperId : Wrapper.Id, "right");
        }


        public async Task Open()
        {
            if (Wrapper == null && WrapperId == null)
            {
                throw new Exception("MenuSystem requires wrapper to be linked if using open/close c# function.");
            }
            await JSRuntime.InvokeVoidAsync("MenuSystemOpen", Wrapper == null ? WrapperId : Wrapper.Id, "right");
        }

        public async Task Close()
        {
            if (Wrapper == null && WrapperId == null)
            {
                throw new Exception("MenuSystem requires wrapper to be linked if using open/close c# function.");
            }
            await JSRuntime.InvokeVoidAsync("MenuSystemClose", Wrapper == null ? WrapperId : Wrapper.Id);
        }
    }
}
