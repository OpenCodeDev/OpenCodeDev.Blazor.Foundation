using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class Label : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// primary, secondary, success, alert or warning
        /// </summary>
        [Parameter]
        public string Color { get; set; }

        /// <summary>
        /// mdi-**** Material Design Icon.
        /// </summary>
        [Parameter]
        public string Icon { get; set; }
    }
}
