﻿using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenCodeDev.Blazor.Foundation.Extensions.RenderFragmentExt;
namespace OpenCodeDev.Blazor.Foundation.Plugins.HighlightCS.Components
{
    public partial class HighlightCode : ComponentBase
    {

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

        /// <summary>
        /// Expand to fill the whole available space.
        /// </summary>
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

        public bool Purge { get; set; }
        public bool FirstRendered { get; set; }
        protected override void OnParametersSet()
        {
            if (!Purge)
            { Purge = true; }
            else
            {
                Purge = false;
            }

        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) FirstRendered = firstRender;

        }

        internal RenderFragment BuildHighlightCS()
        {
            return builder =>
            {
                builder.OpenComponent<HighlightCS>(0);
                builder.AddAttribute(0, "CopyIconColor", CopyIconColor);
                builder.AddAttribute(0, "HeaderBGcolor", HeaderBGcolor);
                builder.AddAttribute(0, "HeaderIcon", HeaderIcon);
                builder.AddAttribute(0, "HeaderIconColor", HeaderIconColor);
                builder.AddAttribute(0, "HeaderTitle", HeaderTitle);
                builder.AddAttribute(0, "HeaderTitleColor", HeaderTitleColor);
                builder.AddAttribute(0, "Full", Full);
                builder.AddAttribute(0, "Content", Content);
                builder.AddAttribute(0, "ContentBytes", ContentBytes);
                builder.AddAttribute(0, "Language", Language);
                builder.CloseComponent();
            };

        }

        [RegisterMarkdown(typeof(HighlightCode), true)]
        public static async Task<MarkdownElement?> FromMarkdown(MarkdownComponent data)
        {
            return new MarkdownElement(p =>
            {
                p.OpenComponent<HighlightCode>(AutoIndex());
                p.AddAttribute(AutoIndex(), nameof(Language), data.GetArgument(nameof(Language), "cs"));
                p.AddAttribute(AutoIndex(), nameof(Full), data.GetArgument(nameof(Full), false));
                p.AddAttribute(AutoIndex(), nameof(Content), data.ChildContent);
                p.CloseComponent();
            }, data.Position);
        }
    }
}
