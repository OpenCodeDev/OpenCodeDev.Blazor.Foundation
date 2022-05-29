using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Extensions;


namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class Button : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// id of Foundation element to toggle can be anything. Modal, Switch, Off-Canvas....
        /// </summary>
        [Parameter]
        public string DataToggle { get; set; }


        [Parameter]
        public bool MarginTop { get; set; } = false;

        [Parameter]
        public bool MarginBottom { get; set; } = true;

        [Parameter]
        public bool MarginLeft { get; set; } = false;

        [Parameter]
        public bool MarginRight { get; set; } = false;

        [Parameter]
        public bool? HorizontalMargin { get; set; } = null;

        [Parameter]
        public bool? VerticalMargin { get; set; } = null;


        [Parameter]
        public bool PaddingTop { get; set; } = true;

        [Parameter]
        public bool PaddingBottom { get; set; } = true;

        [Parameter]
        public bool PaddingLeft { get; set; } = true;

        [Parameter]
        public bool PaddingRight { get; set; } = true;

        [Parameter]
        public bool? HorizontalPadding { get; set; } = null;

        [Parameter]
        public bool? VerticalPadding { get; set; } = null;
        protected override void OnInitialized()
        {
            if (Id != null)
            {
                Id = Guid.NewGuid().HTMLCompliant().ToString();
            }
        }

        protected override void OnParametersSet()
        {
            if (AdditionalAttributes == null) { AdditionalAttributes = new Dictionary<string, object>(); }
            if (!AdditionalAttributes.ContainsKey("style")) { AdditionalAttributes.Add("style", ""); }

            string nStyle = (string)AdditionalAttributes["style"];
            nStyle = GetMargin(VerticalMargin != null ? (bool)VerticalMargin : MarginTop, nStyle, "top");
            nStyle = GetMargin(VerticalMargin != null ? (bool)VerticalMargin : MarginBottom, nStyle, "bottom");
            nStyle = GetMargin(HorizontalMargin != null ? (bool)HorizontalMargin : MarginLeft, nStyle, "left");
            nStyle = GetMargin(HorizontalMargin != null ? (bool)HorizontalMargin : MarginLeft, nStyle, "right");

            nStyle = GetPadding(VerticalPadding != null ? (bool)VerticalPadding : PaddingTop, nStyle, "top");
            nStyle = GetPadding(VerticalPadding != null ? (bool)VerticalPadding : PaddingBottom, nStyle, "bottom");
            nStyle = GetPadding(HorizontalPadding != null ? (bool)HorizontalPadding : PaddingLeft, nStyle, "left");
            nStyle = GetPadding(HorizontalPadding != null ? (bool)HorizontalPadding : PaddingRight, nStyle, "right");
            AdditionalAttributes["style"] = nStyle;
        }


        private string GetMargin(bool ifTrue, string appendto, string margin)
        {
            if (ifTrue)
            {
                return $"margin-{margin}:1rem;{appendto}";
            }
            return $"margin-{margin}:0;{appendto}";
        }

        private string GetPadding(bool ifTrue, string appendto, string margin)
        {
            if (ifTrue)
            {
                return $"padding-{margin}:1rem;{appendto}";
            }
            return $"padding-{margin}:0;{appendto}";
        }


    }
}
