using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine
{
    /// <summary>
    /// The Class representing the Markdown converted element
    /// </summary>
    public class MarkdownElement
    {
        /// <summary>
        /// Not Null with parsed markdown is special blazor support.
        /// </summary>
        public RenderFragment? Fragment { get; private set; }
        /// <summary>
        /// Not null when parsed element is regular markdown turned into HTML.
        /// </summary>
        public MarkupString? Html { get; private set; }

        /// <summary>
        /// Marks the position of the element, the order in witch it should be rendered.
        /// </summary>
        public int Position { get; private set; }
        /// <summary>
        /// Length fomr the position of the captured element from the raw input.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Create with Blazor RenderFragment.
        /// </summary>
        public MarkdownElement(RenderFragment fragment, int position)
        {
            Fragment = fragment;
            Position = position;
        }

        /// <summary>
        /// Create with regular HTML
        /// </summary>
        public MarkdownElement(MarkupString html, int position)
        {
            Html = html;
            Position = position;
        }

    }
}
