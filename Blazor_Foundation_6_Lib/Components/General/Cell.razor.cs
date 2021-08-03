using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.General
{
    public partial class Cell : ComponentBase
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


        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// small-4, auto, shrink, large-auto, small-up-2, medium-up-4, large-up-6 and more...
        /// <br/><br/><see href="https://get.foundation/sites/docs/xy-grid.html#auto-sizing">Documentation</see>
        /// </summary>
        [Parameter]
        public string Size { get; set; }



        protected override void OnInitialized()
        {

        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }
    }
}
