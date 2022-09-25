using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions;


namespace OpenCodeDev.Blazor.Foundation.Plugins.HighlightCS.Components
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
        /// <summary>
        /// String or Base64 Encoded
        /// </summary>
        [Parameter]
        public string Content { get; set; } = null;

        [Parameter]
        public byte[] ContentBytes { get; set; } = null;

        /// <summary>
        /// Default: cs <br/>
        /// Supported: cs, c, cpp, fsharp, golang, html, xml, js, md, php, python <br/>
        /// Using CDN provided in DOC highlightjs support 39 language by default<br/>
        /// See 37 Common Support Language: <a href="https://highlightjs.org/static/demo/" >Demo</a> <br/>
        /// See Also their possible code: <a href="https://github.com/highlightjs/highlight.js/blob/main/SUPPORTED_LANGUAGES.md">Classes</a>
        /// </summary>
        [Parameter]
        public string Language { get; set; } = "cs";
        public Dictionary<string, string[]> Supported { get; set; } = new Dictionary<string, string[]> {
            { "c", new string[] { "mdi-language-c", "#044F88", "I2luY2x1ZGUgPHN0ZGlvLmg+CmludCBtYWluKCkgewogICAvLyBwcmludGYoKSBkaXNwbGF5cyB0aGUgc3RyaW5nIGluc2lkZSBxdW90YXRpb24KICAgcHJpbnRmKCJIZWxsbywgV29ybGQhIik7CiAgIHJldHVybiAwOwp9" } },
            { "cpp", new string[] { "mdi-language-cpp", "#044F88", "I2luY2x1ZGUgPGlvc3RyZWFtPgppbnQgbWFpbigpIHsKICAgIHN0ZDo6Y291dCA8PCAiSGVsbG8gV29ybGQhIjsKICAgIHJldHVybiAwOwp9" } },
            { "cs", new string[] { "mdi-language-csharp", "#178600", "bmFtZXNwYWNlIEhlbGxvV29ybGQKewogICAgY2xhc3MgSGVsbG8geyAgICAgICAgIAogICAgICAgIHN0YXRpYyB2b2lkIE1haW4oc3RyaW5nW10gYXJncykKICAgICAgICB7CiAgICAgICAgICAgIFN5c3RlbS5Db25zb2xlLldyaXRlTGluZSgiSGVsbG8gV29ybGQhIik7CiAgICAgICAgfQogICAgfQp9" } },
            { "css", new string[] { "mdi-language-css3", "#563d7c", "aDEgewoJY29sb3I6IERlZXBTa3lCbHVlOwp9" } },
            { "yaml", new string[] { "mdi-file-cog", "#e44b23", "IHhtYXM6IHRydWUKIGZyZW5jaC1oZW5zOiAzCiBjYWxsaW5nLWJpcmRzOgogICAtIGh1ZXkKICAgLSBkZXdleQogICAtIGxvdWllCiAgIC0gZnJlZA==" } },
            { "sql", new string[] { "mdi-database", "#555555", "Q1JFQVRFIFRBQkxFIGhlbGxvd29ybGQgKHBocmFzZSBURVhUKTsKSU5TRVJUIElOVE8gaGVsbG93b3JsZCBWQUxVRVMgKCJIZWxsbywgV29ybGQhIik7CklOU0VSVCBJTlRPIGhlbGxvd29ybGQgVkFMVUVTICgiR29vZGJ5ZSwgV29ybGQhIik7ClNFTEVDVCBDT1VOVCgqKSBGUk9NIGhlbGxvd29ybGQ7" } },
            { "go", new string[] { "mdi-language-go", "#375eab", "cGFja2FnZSBtYWluCmltcG9ydCAiZm10IgoKZnVuYyBtYWluKCkgewogICBmbXQuUHJpbnRsbigiSGVsbG8sIFdvcmxkISIpCn0=" } },
            { "html", new string[] { "mdi-language-html5", "#e44b23", "PCFET0NUWVBFIGh0bWw+CjxodG1sPgogICAgPGhlYWQ+CiAgICAgICAgPHRpdGxlPkV4YW1wbGU8L3RpdGxlPgogICAgPC9oZWFkPgogICAgPGJvZHk+CiAgICAgICAgPHA+VGhpcyBpcyBhbiBleGFtcGxlIG9mIGEgc2ltcGxlIEhUTUwgcGFnZSB3aXRoIG9uZSBwYXJhZ3JhcGguPC9wPgogICAgPC9ib2R5Pgo8L2h0bWw+" } },
            { "razor", new string[] { "mdi-language-html5", "#5d2b90", "QHVzaW5nIE9wZW5Db2RlRGV2LkJsYXpvci5Gb3VuZGF0aW9uLkRvYy5Db3JlLkNvbXBvbmVudHMuQ29udGFpbmVycy5TZWN0aW9uOwoKPFNlY3Rpb25XcmFwcGVyPgogICAgPFNlY3Rpb25MZWZ0PgoKICAgIDwvU2VjdGlvbkxlZnQ+CjwvU2VjdGlvbldyYXBwZXI+" } },
            { "java", new string[] { "mdi-language-java", "#b07219", "Y2xhc3MgSGVsbG9Xb3JsZCB7CiAgICBwdWJsaWMgc3RhdGljIHZvaWQgbWFpbihTdHJpbmdbXSBhcmdzKSB7CiAgICAgICAgU3lzdGVtLm91dC5wcmludGxuKCJIZWxsbywgV29ybGQhIik7IAogICAgfQp9" } },
            { "js", new string[] { "mdi-language-javascript", "#d4b72a", "bGV0IG5hbWUgPSBwcm9tcHQoIldoYXQgaXMgeW91ciBuYW1lPyIpOw==" } },
            { "kotlin", new string[] { "mdi-language-kotlin", "#F18E33", "cGFja2FnZSBvcmcua290bGlubGFuZy5wbGF5ICAgICAgICAgLy8gMQoKZnVuIG1haW4oKSB7ICAgICAgICAgICAgICAgICAgICAgICAgLy8gMgogICAgcHJpbnRsbigiSGVsbG8sIFdvcmxkISIpICAgICAgICAvLyAzCn0=" } },
            { "lua", new string[] { "mdi-language-lua", "#555555", "LS0gZGVmaW5lcyBhIGZhY3RvcmlhbCBmdW5jdGlvbgpmdW5jdGlvbiBmYWN0IChuKQogICAgaWYgbiA9PSAwIHRoZW4KICAgIHJldHVybiAxCiAgICBlbHNlCiAgICByZXR1cm4gbiAqIGZhY3Qobi0xKQogICAgZW5kCmVuZAoKcHJpbnQoImVudGVyIGEgbnVtYmVyOiIpCmEgPSBpby5yZWFkKCIqbnVtYmVyIikgICAgICAgIC0tIHJlYWQgYSBudW1iZXIKcHJpbnQoZmFjdChhKSk=" } },
            { "markdown", new string[] { "mdi-language-markdown", "#555555", "LS0tCl9fQWR2ZXJ0aXNlbWVudCA6KV9fCgotIF9fW3BpY2FdKGh0dHBzOi8vbm9kZWNhLmdpdGh1Yi5pby9waWNhL2RlbW8vKV9fIC0gaGlnaCBxdWFsaXR5IGFuZCBmYXN0IGltYWdlCiAgcmVzaXplIGluIGJyb3dzZXIuCi0gX19bYmFiZWxmaXNoXShodHRwczovL2dpdGh1Yi5jb20vbm9kZWNhL2JhYmVsZmlzaC8pX18gLSBkZXZlbG9wZXIgZnJpZW5kbHkKICBpMThuIHdpdGggcGx1cmFscyBzdXBwb3J0IGFuZCBlYXN5IHN5bnRheC4KCllvdSB3aWxsIGxpa2UgdGhvc2UgcHJvamVjdHMhCgotLS0KCiMgaDEgSGVhZGluZyA4LSkKIyMgaDIgSGVhZGluZwojIyMgaDMgSGVhZGluZwojIyMjIGg0IEhlYWRpbmcKIyMjIyMgaDUgSGVhZGluZwojIyMjIyMgaDYgSGVhZGluZwo=" } },
            { "php", new string[] { "mdi-language-php", "#4F5D95", "PGh0bWw+CiA8aGVhZD4KICA8dGl0bGU+UEhQIFRlc3Q8L3RpdGxlPgogPC9oZWFkPgogPGJvZHk+CiA8P3BocCBlY2hvICc8cD5IZWxsbyBXb3JsZDwvcD4nOyA/PiAKIDwvYm9keT4KPC9odG1sPg==" } },
            { "python", new string[] { "mdi-language-python", "#3572A5", "cHJpbnQoIlRoaXMgbGluZSB3aWxsIGJlIHByaW50ZWQuIik=" } },
            { "r", new string[] { "mdi-language-r", "#198ce7", "PiBteVN0cmluZyA8LSAiSGVsbG8sIFdvcmxkISIKPiBwcmludCAoIG15U3RyaW5nKQpbMV0gIkhlbGxvLCBXb3JsZCEi" } },
            { "ruby", new string[] { "mdi-language-ruby", "#701516", "aXJiKG1haW4pOjAwMjowPiBwdXRzICJIZWxsbyBXb3JsZCI=" } },
            { "swift", new string[] { "mdi-language-swift", "#ffac45", "Ly8gU3dpZnQgIkhlbGxvLCBXb3JsZCEiIFByb2dyYW0KcHJpbnQoIkhlbGxvLCBXb3JsZCEiKSA=" } },
            { "ts", new string[] { "mdi-language-typescript", "#2b7489", "bGV0IG1lc3NhZ2U6IHN0cmluZyA9ICdIZWxsbywgV29ybGQhJzsKLy8gY3JlYXRlIGEgbmV3IGhlYWRpbmcgMSBlbGVtZW50CmxldCBoZWFkaW5nID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnaDEnKTsKaGVhZGluZy50ZXh0Q29udGVudCA9IG1lc3NhZ2U7Ci8vIGFkZCB0aGUgaGVhZGluZyB0aGUgZG9jdW1lbnQKZG9jdW1lbnQuYm9keS5hcHBlbmRDaGlsZChoZWFkaW5nKTs=" } },
            { "typescript", new string[] { "mdi-language-typescript", "#2b7489", "bGV0IG1lc3NhZ2U6IHN0cmluZyA9ICdIZWxsbywgV29ybGQhJzsKLy8gY3JlYXRlIGEgbmV3IGhlYWRpbmcgMSBlbGVtZW50CmxldCBoZWFkaW5nID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnaDEnKTsKaGVhZGluZy50ZXh0Q29udGVudCA9IG1lc3NhZ2U7Ci8vIGFkZCB0aGUgaGVhZGluZyB0aGUgZG9jdW1lbnQKZG9jdW1lbnQuYm9keS5hcHBlbmRDaGlsZChoZWFkaW5nKTs=" } },
            { "xml", new string[] { "mdi-xml", "#555555", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPHRleHQ+CiAgPHBhcmE+aGVsbG8gd29ybGQ8L3BhcmE+CjwvdGV4dD4=" } },
            { "txt", new string[] { "mdi-code-braces", "#555555", "VGV4dCBBbnkgVGV4dApJbiBIZXJlIGlzIEluIEhlcmU=" } },
            { "json", new string[] { "mdi-language-c", "#555555", "ewogICJzdHVkZW50aWQiIDogMTAxLAogICJmaXJzdG5hbWUiIDogIkpvaG4iLAogICJsYXN0bmFtZSIgOiAiRG9lIiwKICAiaXNTdHVkZW50IiA6IHRydWUsCiAgImNsYXNzZXMiIDogWwogICAgIkJ1c2luZXNzIFJlc2VhcmNoIiwKICAgICJFY29ub21pY3MiLAogICAgIkZpbmFuY2UiCiAgXQp9" } }
        };
        public Dictionary<string, string> LangExceptions { get; set; } = new Dictionary<string, string>
        {
            { "razor", "html" }
        };


        public bool CopyBusy = false;
        private bool HeaderIsReady = false;


        private string DecodedContent { get; set; }
        private string Id { get; set; } = Guid.NewGuid().HTMLCompliant().ToString();



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

            }
            else if (ContentBytes != null)
            {
                return Encoding.Default.GetString(ContentBytes);
            }
            return Localization.Lang.NoContentHighlightCS;
        }

        /// <summary>
        /// true when component should discard content and recreate ??
        /// </summary>
        public bool ShouldReCreate { get; set; }
        public bool FirstRenderDone { get; set; }

        protected override void OnInitialized()
        {
            //Console.WriteLine($"{Language} -> {Supported[Language][1]}");
            Language = !Supported.ContainsKey(Language.ToLower()) ? "txt" : Language;
            DecodedContent = ContentDecodeHandler();
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RenderProperly();
                FirstRenderDone = true;
            }
        }
        public async Task RenderProperly()
        {
            await JS.InvokeVoidAsync("HighlightJSInit", Id);
            HeaderIsReady = true;
            base.StateHasChanged();
        }
    }
}
