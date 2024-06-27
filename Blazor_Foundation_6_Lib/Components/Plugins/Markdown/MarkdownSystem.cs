using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown
{
    /// <summary>
    /// Main controller for Markdown/Blazor Processing.
    /// </summary>
    public class MarkdownSystem
    {

        /// <summary>
        /// Called when requesting component renderfragment.
        /// </summary>
        public static Func<MarkdownComponent, Task<MarkdownElement?>> OnComponentRequest { get; set; }
        private static Dictionary<string, Func<MarkdownComponent, Task<MarkdownElement?>>> ComponentParsers { get; set; } = new();

        /// <summary>
        /// Register a component parser.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public static void RegisterComponent(string name, Func<MarkdownComponent, Task<MarkdownElement?>> action)
        {
            if (ComponentParsers.ContainsKey(name)) {
                Console.WriteLine($"[ERROR] The component {name} is already register for Markdown parsing.");
                return;
            }
            ComponentParsers.Add(name, action);
        }

        /// <summary>
        /// Remove component from parsing.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public static void RemoveComponent(string name, Func<MarkdownComponent, Task<MarkdownElement?>> action)
        {
            if (!ComponentParsers.ContainsKey(name)) return;

            ComponentParsers.Remove(name);
        }

        /// <summary>
        /// Process Raw Markdown Document to HTML + Components when supported.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public async static Task<List<MarkdownElement>> ProcessDocument(string raw)
        {
            List<MarkdownElement> parsedMd = new();
            if (!string.IsNullOrEmpty(raw))
            {
                    Console.WriteLine($"ComponentParsers = {ComponentParsers.Count}");

                // Create MardownParser.
                List<int[]> blazorChoping = new();
                foreach (var component in ComponentParsers) {
                    Console.WriteLine($"Check for {component.Key}");
                    string pattern = @$"\[(\-\-{component.Key}\b[^>]*\-\-)\]((\n|.)*?)\[\-\-\/{component.Key}\-\-\]";
                    try {
                        MatchCollection matches = Regex.Matches(raw, pattern);
                        if (matches.Count > 0)
                            foreach (Match item in matches)
                            {
                                if (!item.Success) continue;
                                blazorChoping.Add(new int[] { item.Index, item.Length });
                                Console.WriteLine($"{item.Index} : {item.Length}");

                                MarkdownElement? mde = await ProcessCode(component.Key, item.Value, item, item.Index);
                                if (mde == null) continue; // ignore failed.
                                parsedMd.Add(mde);
                            }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // Silence Errors
                    }
                }
                Console.WriteLine($"blazorChoping = {blazorChoping.Count}");

                string capture = "";
                int ignoreUntil = -1;
                int position = -1;
                for (int i = 0; i < raw.Length; i++)
                {
                    int index = i;
                    int[]? chop = blazorChoping.Find(p => p[0] == index);
                    if (chop != null)
                    {
                        ignoreUntil = chop[0] + chop[1];
                        if (capture.Length > 0)
                        {
                            parsedMd.Add(new MarkdownElement((MarkupString)CommonMark.CommonMarkConverter.Convert(capture), position));
                        }
                        capture = ""; // reset capture;
                        position = -1;
                    }
                    if (index <= ignoreUntil) continue;
                    if (position == -1) position = index;
                    capture += raw[index];
                }
                if (capture != "")
                    parsedMd.Add(new MarkdownElement((MarkupString)CommonMark.CommonMarkConverter.Convert(capture), position));
            }
            else
            {
                //parsedMd.Add(new MarkdownElement(WarningMessage($"Document: {Location} is empty, misconfigured or could not be fetch."), 0));
            }
            return parsedMd;
        }



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
            rawArgs = rawArgs.Remove(rawArgs.Length - 1, 1); // remove last which = )
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

        private static async Task<MarkdownElement> ProcessCode(string name, string value, Match initialMatch, int position)
        {

            string secondGroup = GetMarkdown2ndGrp(initialMatch);
            Console.WriteLine(secondGroup);
            Dictionary<string, string>? arguments = GetObjectArguments(value, name);
            return await ComponentParsers[name].Invoke(new MarkdownComponent(name, secondGroup, arguments, position));
            //return new MarkdownElement(p =>
            //{
            //    p.OpenComponent<HighlightCode>(AutoIndex());
            //    p.AddAttribute(AutoIndex(), "Language", language);
            //    p.AddAttribute(AutoIndex(), "Content", content);
            //    p.CloseComponent();
            //}, position);
        }

        /// <summary>
        /// Return Second Group usually eg: # = 1st & Header = 2nd
        /// </summary>
        private static string GetMarkdown2ndGrp(Match match)
        {
            if (!match.Success || match.Groups.Count <= 1) throw new Exception("2nd group is missing, incorrect regex or format.");
            return match.Groups[2].Value;
        }

    }
}
