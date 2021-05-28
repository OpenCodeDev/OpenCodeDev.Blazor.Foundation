using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Clipboard
{
    public class Clipboard : IClipboard
    {
        public async Task<string> ReadText(IJSRuntime js)
        {
            return await js.InvokeAsync<string>("ClipboardReadText");
        }

        public async Task SetText(IJSRuntime js, string copy)
        {
            if (copy == null) { throw new Exception("copy cannot be null;"); }
            var b64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(copy));
            await js.InvokeVoidAsync("ClipboardCopyText", b64);
        }
    }
}
