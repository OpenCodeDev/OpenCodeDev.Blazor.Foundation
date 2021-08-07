using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.General
{
    public partial class GridX : ComponentBase
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
        /// Vertical Margin....
        /// </summary>
        [Parameter]
        public bool VerticalMargin { get; set; }

        /// <summary>
        /// Horizontal Margin
        /// </summary>
        [Parameter]
        public bool HorizontalMargin { get; set; }

        /// <summary>
        /// Vertical Padding
        /// </summary>
        [Parameter]
        public bool VerticalPadding { get; set; }

        /// <summary>
        /// Horizontal Padding
        /// </summary>
        [Parameter]
        public bool HorizontalPadding { get; set; }

        protected override void OnInitialized()
        {

        }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

        }
    }
}
