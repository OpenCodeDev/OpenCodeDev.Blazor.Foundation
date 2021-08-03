using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Typography
{
    public partial class Spacing : ComponentBase
    {
        [Parameter]
        public string VerticalSpacing { get; set; } = null;

        [Parameter]
        public string HorizantalSpacing { get; set; } = null;
    }
}
