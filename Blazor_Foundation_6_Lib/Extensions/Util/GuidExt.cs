using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.Util
{
    public static class GuidExt
    {
        /// <summary>
        /// Generate a GUID that comply with HTML5 standards.
        /// </summary>
        public static Guid HTMLCompliant (this Guid guid)
        {
            var b = guid.ToByteArray();
            b[3] |= 0xF0;
            return new Guid(b);
        }
    }
}
