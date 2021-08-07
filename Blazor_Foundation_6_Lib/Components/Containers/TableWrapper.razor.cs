using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class TableWrapper : ComponentBase
    {
        /// <summary>
        /// If True, active hover effect in table rows. (Default: False)
        /// </summary>
        [Parameter]
        public bool Hover { get; set; } = false;

        /// <summary>
        /// If False, remove stripes from all tables. (Default: True)
        /// </summary>
        [Parameter]
        public bool Striped { get; set; } = true;

        /// <summary>
        /// If True, stack the table on small screens. (Default: False)
        /// </summary>
        [Parameter]
        public bool Stacked { get; set; } = false;

        /// <summary>
        /// If True, enable horizontal scrolling in your table. (Default: False)
        /// </summary>
        [Parameter]
        public bool HorizontalScroll { get; set; } = false;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }
    }
}
