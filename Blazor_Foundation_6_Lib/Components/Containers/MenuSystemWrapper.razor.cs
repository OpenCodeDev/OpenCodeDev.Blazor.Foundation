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
    public partial class MenuSystemWrapper : ComponentBase, IDisposable
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
        /// Menu System Options
        /// <br/><br/><see href="https://flawlessloop.com/how-to-use-style-attribute/">How to Use Style</see>
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// AppId for Menu System see documentation.
        /// </summary>
        [Parameter]
        public string AppId { get; set; }

        /// <summary>
        /// True: Will Trigger Registration Automatically Without any Options.
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;


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
                    DataOptions.Add("id", Id);
                    DataOptions.Add("appId", AppId);
                    await JSRuntime.InvokeVoidAsync("MenuSystemRegister", DataOptions != null ? DataOptions.ToJSObjectString() : null);
                }
            }

        }

        public void Dispose()
        {
            if (AutoManaged)
            {
                JSRuntime.InvokeVoidAsync("MenuSystemUnRegister", Id);

            }

        }
    }
}
