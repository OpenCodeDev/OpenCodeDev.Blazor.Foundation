using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins.Markdown
{
    public class MarkdownRule
    {
        public string Regex { get; private set; }
        public Func<string, Match, int,MarkdownElement?>  ProcessValue { get; private set; }

        /// <summary>
        /// Mardown Rule Regex Lookup and Type of Object.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="type"></param>
        /// <param name="processValue">Process Value found by Regex, return MarkdownElement eg. # = H1, ## = H2 and int is position</param>
        public MarkdownRule(string regex, Func<string, Match, int, MarkdownElement?> processValue)
        {
            Regex = regex;
            ProcessValue = processValue;
        }
    }
}
