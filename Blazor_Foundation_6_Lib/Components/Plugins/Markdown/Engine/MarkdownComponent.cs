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


        private string? GetKp(string key) => !Arguments.ContainsKey(key) ? null : Arguments[key];

        public string GetArgument(string key, string def)
        {
            string? item = GetKp(key);
            if (item == null) return def;
            return (string)item;
        }

        public bool GetArgument(string key, bool def) {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = bool.TryParse(item, out bool returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }

        public int GetArgument(string key, int def) {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = int.TryParse(item, out int returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }

        public float GetArgument(string key, float def)
        {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = float.TryParse(item, out float returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }

        public double GetArgument(string key, double def)
        {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = double.TryParse(item, out double returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }

        public long GetArgument(string key, long def)
        {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = long.TryParse(item, out long returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }

        public Guid GetArgument(string key, Guid def)
        {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = Guid.TryParse(item, out Guid returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }

        public char GetArgument(string key, char def)
        {
            string? item = GetKp(key);
            if (item == null) return def;
            bool success = char.TryParse(item, out char returnVal);
            if (!success) returnVal = def;
            return returnVal;
        }
    }
}
