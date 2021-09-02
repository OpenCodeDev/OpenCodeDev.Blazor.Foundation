using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class UtilExt
    {
        /// <summary>
        /// Generate a GUID that comply with HTML5 standards.
        /// </summary>
        public static Guid HTMLCompliant (this Guid guid)
        {
            // Thanks to p.s.w.g on stackoverflow
            var b = guid.ToByteArray();
            b[3] |= 0xF0;
            return new Guid(b);
        }

        public static string ToJSObjectString(this Dictionary<string, object> PluginOptions)
        {
            var listKeypairs = PluginOptions.Select(p => $@"""{p.Key}"": {(p.Value.GetType() == typeof(string) ? $@"""{p.Value}""" : p.Value.ToString().ToLower())}");

            return $@"{{ {String.Join($", ", listKeypairs)} }}";

        }
    }
}
