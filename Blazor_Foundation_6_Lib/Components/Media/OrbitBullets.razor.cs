﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class OrbitBullets : ComponentBase
    {

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }
    }
}
