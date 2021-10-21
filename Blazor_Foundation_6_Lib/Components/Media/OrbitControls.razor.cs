using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class OrbitControls: ComponentBase
    {
        /// <summary>
        /// Content inside previous button mdi-arrow-left-bold (default)
        /// </summary>
        [Parameter]
        public string PreviousContent { get; set; } = "mdi-arrow-left-bold";

        /// <summary>
        /// Add Custom Class to Icon Span Element
        /// </summary>
        [Parameter]
        public string PreviousIconClass { get; set; }

        /// <summary>
        /// Add custom style on icon span element.
        /// </summary>
        [Parameter]
        public string PreviousIconStyle { get; set; }

        /// <summary>
        /// Add Custom Class to button Element
        /// </summary>
        [Parameter]
        public string PreviousButtonClass { get; set; }

        /// <summary>
        /// Add custom style on button element.
        /// </summary>
        [Parameter]
        public string PreviousButtonStyle { get; set; }


        /// <summary>
        /// Content inside next button mdi-arrow-right-bold (default)
        /// </summary>
        [Parameter]
        public string NextContent { get; set; } = "mdi-arrow-right-bold";

        /// <summary>
        /// Add custom style on icon span element.
        /// </summary>
        [Parameter]
        public string NextIconClass { get; set; }

        /// <summary>
        /// Add Custom Class to Icon Span Element
        /// </summary>
        [Parameter]
        public string NextIconStyle { get; set; }

        /// <summary>
        /// Add Custom Class to button Element
        /// </summary>
        [Parameter]
        public string NextButtonClass { get; set; }

        /// <summary>
        /// Add custom style on button element.
        /// </summary>
        [Parameter]
        public string NextButtonStyle { get; set; }


        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }
    }
}
