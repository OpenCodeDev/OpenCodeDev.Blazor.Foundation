using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine
{
    /// <summary>
    /// Register a Mardown Processor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RegisterMarkdownAttribute : Attribute {

        /// <summary>
        /// Name of the component.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Register a Markdown Parser
        /// </summary>
        /// <param name="name">Name of the component (Prefix recommended for any published packages)</param>
        public RegisterMarkdownAttribute(string name)
        {
            Name = name;
        }
    }
}
