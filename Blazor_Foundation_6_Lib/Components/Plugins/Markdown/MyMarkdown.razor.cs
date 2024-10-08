using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Blazor;
using System.Net.Http;
namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown
{
    /// <summary>
    /// This component render Markdown URL or content to be downloaded and redered.
    /// </summary>
    public partial class MyMarkdown : ComponentBase
    {
        /// <summary>
        /// URL to Load MD file.
        /// </summary>
        [Parameter] public string URL { get; set; } = null;
        
        /// <summary>
        /// Content
        /// </summary>
        [Parameter] public string Content { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public List<MarkdownElement> Elements { get; set; } = new List<MarkdownElement>();
        [Inject] private HttpClient _client { get; set; }

        private bool IsLoading { get; set; } = true;
        public string Error { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            if (Elements.Count <= 0)
            {
                if (Content != null) {
                    Elements = await MarkdownSystem.ProcessDocument(Content);
                    IsLoading = false;
                }
                else if (URL != null) {
                    try {
                        var rawDoc = await _client.GetStringAsync(URL);
                        Elements = await MarkdownSystem.ProcessDocument(rawDoc);
                    } catch (Exception ex) { Error = ex.Message; }
                    IsLoading = false;
                }
            }
        }

    }
}
