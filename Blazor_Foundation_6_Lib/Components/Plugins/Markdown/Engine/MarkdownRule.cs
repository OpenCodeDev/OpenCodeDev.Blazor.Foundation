using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine
{
    /// <summary>
    /// Markdown To Component detecting rule
    /// </summary>
    public class MarkdownRule
    {
        /// <summary>
        /// Component Name
        /// </summary>
        public string ComponentName { get; private set; }

        /// <summary>
        /// Pattern to Match
        /// </summary>
        public string Pattern { get; private set; }

        /// <summary>
        /// Action to call
        /// </summary>
        public Func<string, string, Match, int, MarkdownElement?> ProcessComponent { get; private set; }

        /// <summary>
        /// Mardown Rule Regex Lookup and Type of Object.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="name"></param>
        /// <param name="processValue">Process Value found by Regex, return MarkdownElement eg. # = H1, ## = H2 and int is position</param>
        public MarkdownRule(string name, Func<string, string, Match, int, MarkdownElement?> processValue)
        {
            Pattern = @$"\[(\-\-{name}\b[^>]*\-\-)\]((\n|.)*?)\[\-\-\/{name}\-\-\]";
            ProcessComponent = processValue;
            ComponentName = name;
        }
    }
}
