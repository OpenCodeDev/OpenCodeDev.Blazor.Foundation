using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.CreditCard
{
    public partial class CreditCard : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string CardNumber { get; set; }

        [Parameter]
        public string FullName { get; set; }

        [Parameter]
        public string ExpMonth { get; set; }

        [Parameter]
        public string ExpYear { get; set; }

        [Parameter]
        public bool AutoManaged { get; set; } = true;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AutoManaged)
                {
                    await JSRuntime.InvokeVoidAsync("CreditCardRegister", Id, CardNumber, FullName, ExpMonth + '/' + ExpYear);

                }
            }

        }
    }
}
