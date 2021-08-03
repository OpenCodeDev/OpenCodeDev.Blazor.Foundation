using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Extensions.Util;


namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class VerticalSingleSlider : ComponentBase
    {
        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().HTMLCompliant().ToString();

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }


        /// <summary>
        /// Input's id (Only if DataBindingId is undefined)
        /// </summary>
        [Parameter]
        public string InputId { get; set; }

        /// <summary>
        /// Minimum Value
        /// </summary>
        [Parameter]
        public int Min { get; set; } = 1;

        /// <summary>
        /// Max Value
        /// </summary>
        [Parameter]
        public int Max { get; set; } = 100;

        /// <summary>
        /// Current Value (Default: 1)
        /// </summary>
        [Parameter]
        public int Value { get; set; } = 1;

        /// <summary>
        /// True when control is disabled (Default: false)
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// Unit Steps (Default: 1)
        /// </summary>
        [Parameter]
        public int Step { get; set; } = 1;

        /// <summary>
        /// Define if input control is not hidden.
        /// </summary>
        [Parameter]
        public string DataBindingId { get; set; }


        /// <summary>
        /// True: Will Trigger Foundation Script Registration Automatically Without any Options.
        /// </summary>
        [Parameter]
        public bool AutoManaged { get; set; } = true;

        /// <summary>
        /// List of Options to pass when initilization is handled by Blazor. Leave blank if AutoManaged = false.
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DataOptions { get; set; }


        protected override void OnInitialized()
        {
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);



        }
    }
}
