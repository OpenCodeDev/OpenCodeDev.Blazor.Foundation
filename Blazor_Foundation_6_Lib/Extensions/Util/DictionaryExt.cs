using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.Util
{
    public static class DictionaryExt
    {
        public static string ToJSObjectString(this Dictionary<string, object> PluginOptions)
        {
            var listKeypairs = PluginOptions.Select(p => $@"""{p.Key}"": {(p.Value.GetType() == typeof(string) ? $@"""{p.Value}""" : p.Value.ToString().ToLower())}");

            return $@"{{ {String.Join($", ", listKeypairs)} }}";

        }
    }
}
