using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Blazor;

namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class Switch : NewComponentBase
    {
        /// <summary>
        /// Input's ID
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Default value at creation
        /// </summary>
        [Parameter]
        public bool Checked { get; set; }


        [Parameter]
        public EventCallback<bool> OnChangeValue { get; set; }

        [Parameter]
        public EventCallback OnStateChange { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// tiny, small or large (Default: small)
        /// </summary>
        [Parameter]
        public string Size { get; set; } = "small";

        /// <summary>
        /// Radio instead of checkbox
        /// </summary>
        [Parameter]
        public bool IsRadio { get; set; } = false;

        /// <summary>
        /// Group's name where radio belong
        /// </summary>
        [Parameter]
        public string RadioGroup { get; set; }


        /// <summary>
        /// eg: Yes (Default: undefined)
        /// </summary>
        [Parameter]
        public string LabelOn { get; set; }

        /// <summary>
        /// eg: No (Default: undefined)
        /// </summary>
        [Parameter]
        public string LabelOff { get; set; }


        public async Task OnClickHandler()
        {
            if (IsDisabled()) { return; }
            bool nVal = !Checked; Checked = nVal;
            await OnChangeValue.InvokeAsync(Checked);
            await OnStateChange.InvokeAsync(null);
        }
    }
}
