using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Sticky
{
    public partial class StickyTitleBarLeft : ComponentBase
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



        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnInitialized()
        {
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }
    }
}
