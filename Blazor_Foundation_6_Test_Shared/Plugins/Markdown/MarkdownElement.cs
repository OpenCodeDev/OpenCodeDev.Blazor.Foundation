using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins.Markdown
{
    public class MarkdownElement
    {
        public RenderFragment Fragment { get; private set; }

        /// <summary>
        /// Marks the position of the element, the order in witch it should be rendered.
        /// </summary>
        public int Position { get; private set; }

        public MarkdownElement(RenderFragment fragment, int position)
        {
            Fragment = fragment;
            Position = position;
        }

    }
}
