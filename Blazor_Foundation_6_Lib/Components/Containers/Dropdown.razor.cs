using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions;

namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class Dropdown : ComponentBase, IDisposable
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
        /// left, right, top or bottom
        /// </summary>
        [Parameter]
        public string Position { get; set; }

        /// <summary>
        /// top, middle, left, right or bottom
        /// </summary>
        [Parameter]
        public string DataAlignment { get; set; }

        /// <summary>
        /// If false, you will have to create initializer for this Foundation 6 component yourself in javascript. (Default: True)
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;
       
        public enum Behavior
        {
            Hover,
            Dropdown
        }

        [Parameter]
        public Behavior CurrentBehavior { get; set; } = Behavior.Dropdown;

        /// <summary>
        /// List of Options to pass when initilization is handled by Blazor. Leave blank if AutoManaged = false.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnInitialized()
        {


            if (Position == null) { Position = $"position-left"; }
            if (DataAlignment == null) { DataAlignment = $"bottom"; }

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
                    await JSRuntime.InvokeVoidAsync("DropdownRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
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

        public async Task TriggerToggle()
        {
            await JSRuntime.InvokeVoidAsync("ElementToggle", Id);
        }

        public async Task TriggerOpen()
        {
            await JSRuntime.InvokeVoidAsync("ElementOpen", Id);
        }

        public async Task TriggerClose()
        {
            await JSRuntime.InvokeVoidAsync("ElementClose", Id);
        }
    }
}
