using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class ProgressBar : ComponentBase
    {
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
        /// primary (default), secondary, success, warning and alert or custom class
        /// </summary>
        [Parameter]
        public string ColorClass { get; set; } = "primary";

        [Parameter]
        public float Progress { get; set; } = 0;

        /// <summary>
        /// False hide the % text inside the progress bar (default)
        /// </summary>
        [Parameter]
        public bool ShowText { get; set; } = false;
    }
}
