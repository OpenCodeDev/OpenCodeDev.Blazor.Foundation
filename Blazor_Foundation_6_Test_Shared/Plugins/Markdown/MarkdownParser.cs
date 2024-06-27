using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json.Linq;
using OpenCodeDev.Blazor.Foundation.Plugins.HighlightCS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static OpenCodeDev.Blazor.Foundation.Extensions.RenderFragmentExt;
using static System.Net.Mime.MediaTypeNames;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins.Markdown
{
    public class MarkdownParser
    {
        public List<MarkdownRule> MdRules { get; set; } = new List<MarkdownRule>() {
            new MarkdownRule(@"\[(\-\-HighlightCode\b[^>]*\-\-)\]((\n|.)*?)\[\-\-\/HighlightCode\-\-\]", ProcessCodeHighlight),
        };
 



        /// <summary>
        /// Return null if no arguments
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Dictionary<string, string>? GetObjectArguments(string text, string objectName)
        {
            string pattern = @$"\[\-\-{objectName}\s*(\(.*?\))?\-\-\]";
            Match exp = Regex.Match(text, pattern);
            if (!exp.Success || exp.Groups.Count <= 1) return null; // no arguments
            // arguments potentially found?
            var obj = new Dictionary<string, string>();
            string rawArgs = exp.Groups[1].Value; // get (key="value", key="value", key="value")
            rawArgs = rawArgs.TrimStart().TrimEnd(); // remove space around (key="value", key="value")
            rawArgs = rawArgs.Remove(0, 1); // remove 0 which = (
            rawArgs = rawArgs.Remove(rawArgs.Length-1, 1); // remove last which = )
            rawArgs = rawArgs.TrimStart().TrimEnd(); // remove space around key="value", key="value"
            string[] grossArgs = rawArgs.Split(','); // split key="value", key="value" to array of key="value"
            foreach (var item in grossArgs)
            {
                string tempWhole = item.TrimStart().TrimEnd(); // remove space around key="value"
                string[] tempKeyValSplit = tempWhole.Split('='); // split key="value" to key "value"
                tempKeyValSplit[1] = tempKeyValSplit[1].Remove(0, 1); // remove 0 which = "
                tempKeyValSplit[1] = tempKeyValSplit[1].Remove(tempKeyValSplit[1].Length - 1, 1); // remove last which = "
                obj.Add(tempKeyValSplit[0], tempKeyValSplit[1]);
            }
            return obj;
        }

        private static MarkdownElement? ProcessCodeHighlight(string value, Match initialMatch, int position)
        {
            
            string objectName = "HighlightCode";
            string secondGroup = GetMarkdown2ndGrp(initialMatch);
            Dictionary<string, string>? arguments = GetObjectArguments(value, objectName);
            string language = "cs", content = secondGroup;
            if (arguments == null) Console.WriteLine($"{objectName} has no arguments.");
            else
            {
                if(!arguments.ContainsKey("Language"))
                    Console.WriteLine($"{objectName} is missing language cs is default.");

            }
            return new MarkdownElement(p =>
            {
                p.OpenComponent<HighlightCode>(AutoIndex());
                p.AddAttribute(AutoIndex(), "Language", language);
                p.AddAttribute(AutoIndex(), "Content", content);
                p.CloseComponent();
            }, position);
        }
        /// <summary>
        /// Return Second Group usually eg: # = 1st & Header = 2nd
        /// </summary>
        private static string GetMarkdown2ndGrp(Match match)
        {
            if (!match.Success || match.Groups.Count <= 1) throw new Exception("2nd group is missing, incorrect regex or format.");
            return match.Groups[2].Value;
        }

        private static MarkdownElement? ProcessBold(string value, Match initialMatch, int position)
        {
            string secondGroup = GetMarkdown2ndGrp(initialMatch);

            string trimmed = value.TrimStart();
            string? typeofheader = "b";
            return new MarkdownElement(p =>
            {
                p.OpenElement(AutoIndex(), typeofheader);
                p.AddContent(AutoIndex(), secondGroup);
                p.CloseElement();
            }, position);
        }

        private static MarkdownElement? ProcessHeader(string value, Match initialMatch, int position)
        {
            string secondGroup = GetMarkdown2ndGrp(initialMatch);

            string trimmed = value.TrimStart();
            string? typeofheader = "h1";
            // find the correct header.
            if (trimmed.StartsWith("######")) typeofheader = "h6";
            else if (trimmed.StartsWith("#####")) typeofheader = "h5";
            else if (trimmed.StartsWith("####")) typeofheader = "h4";
            else if (trimmed.StartsWith("###")) typeofheader = "h3";
            else if (trimmed.StartsWith("##")) typeofheader = "h2";
            else if (trimmed.StartsWith("#")) typeofheader = "h1";
            else return null; // Ignore

            return new MarkdownElement(p =>
            {
                p.OpenElement(AutoIndex(), typeofheader);
                p.AddContent(AutoIndex(), secondGroup);
                p.CloseElement();
            }, position);
        }
   
    
    }
}
