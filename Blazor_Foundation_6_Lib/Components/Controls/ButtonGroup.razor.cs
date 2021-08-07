using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class ButtonGroup : ComponentBase
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
        /// Size of the buttons.
        /// <b>tiny</b>, <b>small</b> or <b>large</b> (Default: null)
        /// <br/><br/><see href="https://get.foundation/sites/docs/button-group.html#sizing">Documentation</see>
        /// </summary>
        [Parameter]
        public string Size { get; set; }

        /// <summary>
        /// Color of all buttons.
        /// <b>primary</b>, <b>secondary</b>, <b>success</b>, <b>warning</b> or <b>alert</b>. (Default: null)
        /// <br/><br/><see href="https://get.foundation/sites/docs/button-group.html#coloring">Documentation</see>
        /// </summary>
        [Parameter]
        public string Color { get; set; }

        /// <summary>
        /// True: Removes gaps between buttons (Default: False)
        /// <br/><br/><see href="https://get.foundation/sites/docs/button-group.html#no-gaps">Documentation</see>
        /// </summary>
        [Parameter]
        public bool NoGaps { get; set; }

        /// <summary>
        /// True: Full Width Evenly Distributed. (Default: False)
        /// <br/><br/><see href="https://get.foundation/sites/docs/button-group.html#even-width-group">Documentation</see>
        /// </summary>
        [Parameter]
        public bool FullEvenWidth { get; set; }

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
