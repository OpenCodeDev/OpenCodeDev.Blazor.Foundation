using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class Callout : ComponentBase
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
        /// Custom Class to Append at the end of default Foundation Class (eg. "close-button custom").
        /// </summary>
        [Parameter]
        public string CloseButtonClass { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class (eg. "close-button custom").
        /// </summary>
        [Parameter]
        public EventCallback CloseButtonCallBack { get; set; }

        /// <summary>
        /// fade-out, slide-out-up, slide-out-left, slide-out-down, slide-out-right
        /// hinge-out-from-top, hinge-out-from-right, hinge-out-from-up, hinge-out-from-down
        /// hinge-out-from-middle-x, hinge-out-from-middle-y, scale-out-up, scale-out-down
        /// spin-in, spin-in-ccw
        /// </summary>
        [Parameter]
        public string ClosableData { get; set; }

        /// <summary>
        /// True when close button is visible.
        /// </summary>
        [Parameter]
        public bool CloseButton { get; set; } = false;

        /// <summary>
        ///  secondary, primary, success, warning OR alert
        /// </summary>
        [Parameter]
        public string TypeClass { get; set; }

        /// <summary>
        /// small OR large
        /// </summary>
        [Parameter]
        public string Size { get; set; }

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
