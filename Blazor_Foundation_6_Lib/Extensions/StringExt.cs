using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class StringExt
    {
        /// <summary>
        /// Return string with capital letter on each word.
        /// </summary>
        /// <returns></returns>
        public static string ToCapitalized(this string title)
        {
            TextInfo CultureTxtInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            return CultureTxtInfo.ToTitleCase(title);
        }
    }
}
