using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Components.Controls
{
    public partial class InputCreditCard : ComponentBase 
    {
        private string _Card = "";

        /// <summary>
        /// Bind value
        /// </summary>
        [Parameter]
        public string Card { get => _Card; set { _Card = value; } }

        [Parameter]
        public EventCallback<string> CardChanged { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

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

        /// <summary>
        /// Onkeyup event
        /// </summary>
        private async Task Keyup(KeyboardEventArgs args)
        {
            if (OnChangeDelayUpdate || !OnChangeAcceptKey)
            {
                BackingNumber = FormattedCardNumber(BackingNumber.Replace(" ", String.Empty));
                StateHasChanged();
            }
            if (BackingNumber != default)
            {
                Card = BackingNumber.Replace(" ", String.Empty); // Entry without spaces
                await CardChanged.InvokeAsync(Card);
            }
            // Reset the state for the next key
            OnChangeRemovedSomething = false;
            OnChangeAcceptKey = true;
            OnChangeDelayUpdate = true;
            await OnKeyup.InvokeAsync(args);
        }

        /// <summary>
        /// OnKeydown event
        /// </summary>
        private async Task Keydown(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs args)
        {
            //LastBackingNumber = BackingNumber;
            //BackingNumber = "";
            if (Numbers.Contains(args.Key) && (Card == default || Card.Length < 19))
            {
                OnChangeAcceptKey = true;
                OnChangeDelayUpdate = false;
            }
            else if (args.Key == "Backspace")
            {
                OnChangeAcceptKey = true;
                OnChangeRemovedSomething = true;
            }
            else
            {
                OnChangeAcceptKey = false; // Do not accept the next modification
            }
            await OnKeydown.InvokeAsync(args);
        }

        /// <summary>
        /// OnInput Event
        /// </summary>
        private async Task Input(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            if (!OnChangeAcceptKey) { BackingNumber += " "; return; }

            string currentValue = (string)args.Value;
            // Find if space was removed, spaces should be managed by the code not by the user.
            //TODO: Maybe find more efficient way of achieving it?
            if (OnChangeRemovedSomething)
            {
                var curValList = currentValue.ToArray();
                for (int i = 0; i < curValList.Length; i++)
                {
                    int index = i; // 0000 0: 000000
                    if (BackingNumber[index] == ' ' && curValList[index] != ' ') { OnChangeDelayUpdate = true; }

                }
            }

            BackingNumber = currentValue;
            if (!OnChangeDelayUpdate)
            {
                BackingNumber = FormattedCardNumber(BackingNumber.Replace(" ", String.Empty));
            }
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
