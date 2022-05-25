using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Plugins.HighlightCS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins
{
    public class MarkdownParser
    {
        public Dictionary<string, object> Rules { get; set; } = new Dictionary<string, object>() {
            {@"[--HighlightCode\b[^>]*--\]((\n|.)*?)\[--\/HighlightCode--\]", typeof(HighlightCode) },
            {@"#{6}\s?([^\n]+)", "h6"},
            {@"#{5}\s?([^\n]+)", "h5"},
            {@"#{4}\s?([^\n]+)", "h4"},
            {@"#{3}\s?([^\n]+)", "h3"},
            {@"#{2}\s?([^\n]+)", "h2"},
            {@"#{1}\s?([^\n]+)", "h1"},
            
        };
    }
}
