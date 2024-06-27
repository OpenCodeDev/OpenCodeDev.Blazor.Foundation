using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine
{
    /// <summary>
    /// The markdown representation of the components.
    /// </summary>
    public class MarkdownComponent
    {
        /// <summary>
        /// Name of the component.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Nested content.
        /// </summary>
        public string ChildContent { get; private set; }
        /// <summary>
        /// Arguments for the component.
        /// </summary>
        public Dictionary<string, string> Arguments { get; private set; }

        /// <summary>
        /// Element Position in chain
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Create the markdown representation of a component.
        /// </summary>
        public MarkdownComponent(string name, string childContent, Dictionary<string, string> arguments, int position) {
            Name = name;
            ChildContent = childContent;
            Arguments = arguments;
            Position = position;
        }
    }
}
