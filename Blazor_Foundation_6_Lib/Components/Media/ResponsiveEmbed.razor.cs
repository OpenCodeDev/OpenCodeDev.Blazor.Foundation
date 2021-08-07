using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class ResponsiveEmbed : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
