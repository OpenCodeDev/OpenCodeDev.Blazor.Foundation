using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Blazor;
using OpenCodeDev.Blazor.Foundation.Extensions;


namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class Button : NewComponentBase
	{
        [Parameter]
        public RenderFragment ChildContent { get; set; }


        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; } = String.Empty;

        /// <summary>
        /// id of Foundation element to toggle can be anything. Modal, Switch, Off-Canvas....
        /// </summary>
        [Parameter]
        public string DataToggle { get; set; }


        [Parameter]
        public bool? MarginTop { get; set; } = null;

        [Parameter]
        public bool? MarginBottom { get; set; } = null;

        [Parameter]
        public bool? MarginLeft { get; set; } = null;

        [Parameter]
        public bool? MarginRight { get; set; } = null;

        [Parameter]
        public bool? HorizontalMargin { get; set; } = null;

        [Parameter]
        public bool? VerticalMargin { get; set; } = null;


        [Parameter]
        public bool? PaddingTop { get; set; } = null;

        [Parameter]
        public bool? PaddingBottom { get; set; } = null;

        [Parameter]
        public bool? PaddingLeft { get; set; } = null;

        [Parameter]
        public bool? PaddingRight { get; set; } = null;

        [Parameter]
        public bool? HorizontalPadding { get; set; } = null;

        [Parameter]
        public bool? VerticalPadding { get; set; } = null;


        protected override void OnInitialized()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid().HTMLCompliant().ToString();
            }
        }

        protected override void OnParametersSet()
        {
            if (AdditionalAttributes == null) { AdditionalAttributes = new Dictionary<string, object>(); }
            if (!AdditionalAttributes.ContainsKey("style")) { AdditionalAttributes.Add("style", ""); }

            string nClass = Class + " ";
            nClass += GetMarginClass(MarginTop, "top");
            nClass += GetMarginClass(MarginBottom, "bottom");
            nClass += GetMarginClass(MarginLeft, "left");
            nClass += GetMarginClass(MarginLeft,  "right");
            nClass += GetMarginClass(HorizontalMargin, "horizontal");
            nClass += GetMarginClass(VerticalMargin, "vertical");

            nClass += GetPaddingClass(PaddingTop, "top");
            nClass += GetPaddingClass(PaddingBottom, "bottom");
            nClass += GetPaddingClass(PaddingLeft, "left");
            nClass += GetPaddingClass(PaddingRight, "right");
            nClass += GetPaddingClass(HorizontalPadding, "horizontal");
            nClass += GetPaddingClass(VerticalPadding, "vertical");
            Class = nClass;
        }


        private string GetMarginClass(bool? ifTrue, string direction)
        {
            //Ignore to default
            if(ifTrue == null) { return String.Empty; }
            bool final = (bool)ifTrue;
            if (final)
            {
                return $"bf-margin-{direction} ";
            }
            return $"bf-no-margin-{direction} ";
        }

        private string GetPaddingClass(bool? ifTrue, string direction)
        {
            //Ignore to default
            if (ifTrue == null) { return String.Empty; }
            bool final = (bool)ifTrue;
            if (final)
            {
                return $"bf-padding-{direction} ";
            }
            return $"bf-no-padding-{direction} ";
        }


    }
}
