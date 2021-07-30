using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace OpenCodeDev.Blazor.Foundation.Components.Containers
{
    public partial class AccordionItem : ComponentBase
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
        /// Title of the element.
        /// </summary>
        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class (eg. "accordion-title custom").
        /// </summary>
        [Parameter]
        public string TitleClass { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class (eg. "accordion-content custom").
        /// </summary>
        [Parameter]
        public string ContentClass { get; set; }

        /// <summary>
        /// True if it is the first active element on load (Default: False)
        /// </summary>
        [Parameter]
        public bool Active { get; set; } = false;

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
