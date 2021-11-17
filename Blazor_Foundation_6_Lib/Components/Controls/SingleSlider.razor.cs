using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions;


namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class SingleSlider : ComponentBase, IDisposable
    {
        /// <summary>
        /// Unique HTML Identifier.
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Custom Class to Append at the end of default Foundation Class.
        /// </summary>
        [Parameter]
        public string Class { get; set; } = "";

        /// <summary>
        /// Must define an Id for the slider and the external input's id must be the slider's id with -input like 'mysilder-input'<br/>
        /// True = Will rely on external input (if input not found, bind-Value wont update on C#)
        /// </summary>
        [Parameter]
        public bool HasExtInput { get; set; }

        /// <summary>
        /// Minimum Value
        /// </summary>
        [Parameter]
        public float Min { get; set; } = 1;

        /// <summary>
        /// Max Value
        /// </summary>
        [Parameter]
        public float Max { get; set; } = 100;

        /// <summary>
        /// Current Value (Default: 1)
        /// </summary>
        [Parameter]
        public float Value { get; set; } = 1;


        [Parameter]
        public EventCallback<float> ValueChanged { get; set; }

        /// <summary>
        /// True when control is disabled (Default: false)
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// Is Vertical?
        /// </summary>
        [Parameter]
        public bool IsVertical { get; set; } = false;


        /// <summary>
        /// Unit Steps (Default: 1)
        /// </summary>
        [Parameter]
        public float Step { get; set; } = 1;

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


        public static Dictionary<string, SingleSlider> GlobalSliderList = new Dictionary<string, SingleSlider>();

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void OnInitialized()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid().HTMLCompliant().ToString();
                if (HasExtInput)
                {
                    HasExtInput = false;
                    Console.WriteLine("Slider Error, 'HasExtInput=true' but no Id defined. We reset HasExtInput to false and generated an id to avoid bind issues.");
                }
            }
            GlobalSliderList.Add(Id, this);
        }

        [JSInvokable("UpdateSingleSliderValue")]
        public static async Task OnValueUpdateJS(string id, float value)
        {

            if (GlobalSliderList.ContainsKey(id))
            {
                await GlobalSliderList[id].SliderValueChanged(value);
            }
            // Else, Ignore Slider no longer exist.
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);



        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AutoManaged)
                {
                    await JSRuntime.InvokeVoidAsync("SliderRegister", Id, DataOptions != null ? DataOptions.ToJSObjectString() : null);
                }

            }

        }


        public async Task SliderValueChanged(float value)
        {
            Value = value;
            await ValueChanged.InvokeAsync(value);
        }

        public void Dispose()
        {
            if (AutoManaged)
            {
                JSRuntime.InvokeVoidAsync("FoundationDestroy", Id);
            }
            GlobalSliderList.Remove(Id);
        }
    }
}
