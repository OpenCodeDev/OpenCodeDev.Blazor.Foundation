using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.Clipboard
{
    public class Clipboard : IClipboard
    {
        IJSRuntime JSRuntime { get; set; }
        public Clipboard(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        public async Task<string> ReadText(IJSRuntime js)
        {
            return await js.InvokeAsync<string>("ClipboardReadText");
        }

        public async Task SetText(IJSRuntime js, string copy)
        {
            if (copy == null) { throw new Exception("copy cannot be null;"); }
            await js.InvokeVoidAsync("ClipboardCopyText", copy);
        }

        public async Task SetText(string copy)
        {
            if (copy == null) { throw new Exception("copy cannot be null;"); }
            var b64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(copy));
            await JSRuntime.InvokeVoidAsync("ClipboardCopyText", b64);
        }
    }
}
