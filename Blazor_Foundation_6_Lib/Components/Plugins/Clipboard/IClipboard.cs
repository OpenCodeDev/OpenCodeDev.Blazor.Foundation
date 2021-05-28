using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Clipboard
{
   public interface IClipboard
    {
        /// <summary>
        /// Get current text in clipboard.
        /// </summary>
        /// <param name="js">JSRuntime Instance in Balzor</param>
        /// <returns></returns>
        Task<string> ReadText(IJSRuntime js);

        /// <summary>
        /// Set new text to clipboard.
        /// </summary>
        /// <param name="js">JSRuntime instance in blazor</param>
        /// <param name="copy">Value to put in clipboard.</param>
        /// <returns></returns>
        Task SetText(IJSRuntime js, string copy);
    }
}
