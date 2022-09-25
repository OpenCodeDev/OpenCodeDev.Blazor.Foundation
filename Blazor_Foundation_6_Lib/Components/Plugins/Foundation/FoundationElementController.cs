using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Foundation
{
    public class FoundationElementController : IFoundationElementController
    {
        private IJSRuntime JS { get; set; }

        public FoundationElementController(IJSRuntime jS) { JS = jS; }

        public async Task Close(string id)
        {
            await JS.InvokeVoidAsync("ElementClose", id);
        }

        public async Task Open(string id)
        {
            await JS.InvokeVoidAsync("ElementOpen", id);
        }

        public async Task Toggle(string id)
        {
            await JS.InvokeVoidAsync("ElementToggle", id);
        }
    }
}
