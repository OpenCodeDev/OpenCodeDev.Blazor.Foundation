﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.General
{
    public partial class GridContainer : ComponentBase
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
        /// True: Full width of the available space and keep padding. (Default: False)
        /// </summary>
        [Parameter]
        public bool Fluid { get; set; }

        /// <summary>
        /// True: Full width of the available space and remove grid container padding. (Default: False)
        /// </summary>
        [Parameter]
        public bool Full { get; set; }



        protected override void OnInitialized()
        {

        }
    }
}
