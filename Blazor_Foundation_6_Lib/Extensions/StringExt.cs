using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        /// <summary>
        /// Calculate Checksum to readable CRC
        /// </summary>
        /// <param name="data"></param>
        public static string ToCRC32(this string data)
        {
            var crc32 = new System.IO.Hashing.Crc32();
            var bytes = Encoding.UTF8.GetBytes(data);
            crc32.Append(bytes);
            var checkSum = crc32.GetCurrentHash();
            Array.Reverse(checkSum);
            return BitConverter.ToString(checkSum).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Calculate Checksum to readable CRC
        /// </summary>
        /// <param name="data"></param>
        public static string ToCRC64(this string data)
        {
            var crc32 = new System.IO.Hashing.Crc64();
            var bytes = Encoding.UTF8.GetBytes(data);
            crc32.Append(bytes);
            var checkSum = crc32.GetCurrentHash();
            Array.Reverse(checkSum);
            return BitConverter.ToString(checkSum).Replace("-", "").ToLower();
        }
    }
}
