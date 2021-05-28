using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
    public static class JObjectExtension
    {
        public static string ToInlineCSS(this JObject jo)
        {
            string css = "";
            if(jo == null) { return ""; }
            foreach (var item in jo)
            {
                css += $"{item.Key}:{item.Value};";
            }
            return css;
        }
    }
}
