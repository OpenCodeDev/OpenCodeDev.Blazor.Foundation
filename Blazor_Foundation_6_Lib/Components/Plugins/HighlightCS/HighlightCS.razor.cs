using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions.Util;


namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.HighlightCS
{
    public partial class HighlightCS : ComponentBase
    {
        private string CopyIcon { get; set; } = "mdi-content-copy";

        [Parameter]
        public string CopyIconColor { get; set; } = null;

        [Parameter]
        public string HeaderBGcolor { get; set; } = null;

        [Parameter]
        public string HeaderIcon { get; set; } = null;

        [Parameter]
        public string HeaderIconColor { get; set; } = null;


        [Parameter]
        public string HeaderTitle { get; set; } = null;

        [Parameter]
        public string HeaderTitleColor { get; set; } = null;

        [Parameter]
        public bool Full { get; set; } = false;

        public bool CopyBusy = false;
        private bool HeaderIsReady = false;

        /// <summary>
        /// String or Base64 Encoded
        /// </summary>
        [Parameter]
        public string Content { get; set; } = null;

        /// <summary>
        /// Default: cs <br/>
        /// Supported: cs, c, cpp, fsharp, golang, html, xml, js, md, php, python <br/>
        /// Using CDN provided in DOC highlightjs support 39 language by default<br/>
        /// See 37 Common Support Language: <a href="https://highlightjs.org/static/demo/" >Demo</a> <br/>
        /// See Also their possible code: <a href="https://github.com/highlightjs/highlight.js/blob/main/SUPPORTED_LANGUAGES.md">Classes</a>
        /// </summary>
        [Parameter]
        public string Language { get; set; } = "cs";

        private string Id { get; set; } = Guid.NewGuid().HTMLCompliant().ToString();


        private string DecodedContent { get; set; }

        private string ContentDecodeHandler()
        {
            if (Content != null)
            {
                try
                {
                    return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Content));
                }
                catch (Exception)
                {
                    return Content; // Not B64 PRobably Plain Content Passed down.
                }

                //if (isB64)
                //{
                //    return System.Text.Encoding.UTF8.GetString(buffer.ToArray());
                //}
                //else
                //{
                //    return Content; // Not B64 PRobably Plain Content Passed down.
                //}

            }
            return "No Content Provided";
        }

        protected override void OnInitialized()
        {
            DecodedContent = ContentDecodeHandler();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await JS.InvokeVoidAsync("HighlightJSInit", Id);
                HeaderIsReady = true;
                Console.WriteLine("sda");
                StateHasChanged();
            }
        }
    }
}
