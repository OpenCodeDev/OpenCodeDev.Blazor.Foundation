using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine;
using static OpenCodeDev.Blazor.Foundation.Extensions.RenderFragmentExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;

namespace OpenCodeDev.Blazor.Foundation.Components.Media
{
    public partial class Headline : ComponentBase
    {

        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; }


        [RegisterMarkdown(typeof(Headline), true)]
        public static async Task<MarkdownElement?> FromMarkdown(MarkdownComponent data)
        {
            return new MarkdownElement(p =>
            {
                p.OpenComponent<Headline>(AutoIndex());
                p.AddAttribute(AutoIndex(), nameof(ChildContent), new RenderFragment(delegate (RenderTreeBuilder __builder2)
                {
                    __builder2.AddMarkupContent(AutoIndex(), data.ChildContent);
                }));
                p.CloseComponent();
            }, data.Position);
        }
    }
}
