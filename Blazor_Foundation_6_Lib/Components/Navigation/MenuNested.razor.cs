using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Navigation
{
    public partial class MenuNested : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// True when menu direction is vertical. (Default: False)
        /// </summary>
        [Parameter]
        public bool Vertical { get; set; } = false;

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }


        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }
    }
}
