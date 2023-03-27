using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Blazor;

namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class InputCreditCard : NewComponentBase
    {
        private string _Card = "";

        /// <summary>
        /// Bind value
        /// </summary>
        [Parameter]
        public string Card { get => _Card; set { _Card = value; } }

        [Parameter]
        public EventCallback<string> CardChanged { get; set; }


        /// <summary>
        /// Max card Length Allowed (Default: 16)
        /// </summary>
        [Parameter]
        public int CardLength { get; set; } = 16;
        /// <summary>
        /// Called when event keyup is triggered
        /// </summary>
        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyup { get; set; }

        /// <summary>
        /// Called when keydown is triggered
        /// </summary>
        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeydown { get; set; }

        /// <summary>
        /// Called when oninput is triggered
        /// </summary>
        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }

        //TODO: Allow paste as a feature. Maybe capture CTRL+V and filter data ?
        //[Parameter]
        //public bool AllowPaste { get; set; } = false; 

        /// <summary>
        /// Current binding (with format)
        /// </summary>
        private string BackingNumber { get; set; } = "";

        /// <summary>
        /// Allowed character (keyed by user)
        /// </summary>
        private List<string> Numbers { get; set; } = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        /// <summary>
        /// When true input format will be updated onkeyup
        /// </summary>
        private bool OnChangeDelayUpdate { get; set; } = true;

        /// <summary>
        /// When false, the input will refuse to key in the next onchange character.
        /// </summary>
        private bool OnChangeAcceptKey { get; set; } = true;

        /// <summary>
        /// When true, onchange will make sure no space was removed by the user
        /// </summary>
        private bool OnChangeRemovedSomething { get; set; } = false;


        protected override void OnParametersSet()
        {
            BackingNumber = Card;            
        }
 

 
        /// <summary>
        /// OnInput Event
        /// </summary>
        private async Task Input(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            //if (!OnChangeAcceptKey) { BackingNumber += " "; return; }

            string currentValue = String.Concat(((string)args.Value).Where(p=> Numbers.Contains($"{p}")).Take(CardLength));
            BackingNumber = currentValue;
            Card = BackingNumber;
            await CardChanged.InvokeAsync(BackingNumber);

            StateHasChanged();
            await OnChange.InvokeAsync(args);
        }

        /// <summary>
        /// Format to Credit Card Format
        /// </summary>
        public string FormattedCardNumber(string num)
        {
            string formatted = num; // 0, 1, 2, 3

            if (num.Length > 4 && num.Length <= 8)
            {
                formatted = string.Format("{0} {1}", num.Substring(0, 4), num.Substring(4));
            }
            else if (num.Length > 8 && num.Length <= 12)
            {
                formatted = string.Format("{0} {1} {2}", num.Substring(0, 4), num.Substring(4, 4), num.Substring(8));
            }
            else if (num.Length > 12 && num.Length <= 16)
            {
                formatted = string.Format("{0} {1} {2} {3}", num.Substring(0, 4), num.Substring(4, 4), num.Substring(8, 4), num.Substring(12));
            }
            else if (num.Length > 16)
            {
                formatted = string.Format("{0} {1} {2} {3} {4}", num.Substring(0, 4), num.Substring(4, 4), num.Substring(8, 4), num.Substring(12, 4), num.Substring(16));
            }
            return formatted;
        }
    }
}
