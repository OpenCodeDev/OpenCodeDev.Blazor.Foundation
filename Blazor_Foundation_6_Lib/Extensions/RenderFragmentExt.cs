using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class RenderFragmentExt
    {
        /// <summary>
        /// AutoIndex with Line NB and Track Reference thru indexer
        /// </summary>
        public static int AutoIndex(ref int indexer, [CallerLineNumber] int index = 0)
        {
            indexer = index;
            return index;
        }
        /// <summary>
        /// AutoIndex with Line NB without tracking
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int AutoIndex([CallerLineNumber] int index = 0)
        {
            return index;
        }


    }
}
