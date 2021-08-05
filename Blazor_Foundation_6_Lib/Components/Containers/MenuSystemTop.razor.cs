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
    public partial class MenuSystemTop : ComponentBase
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
        ///
        [Parameter]
        [Obsolete("The wrapper for the top menu will be removed in the next update. Note: Wrapper for top menu is completely useless and unsed by system itself.")]
        public MenuSystemWrapper Wrapper { get; set; }


        protected override void OnInitialized()
        {
        }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);


        }
    }
}
