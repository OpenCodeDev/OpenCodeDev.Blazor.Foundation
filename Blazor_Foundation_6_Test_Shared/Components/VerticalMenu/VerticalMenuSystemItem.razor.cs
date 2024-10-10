using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Components.VerticalMenu
{
    public partial class VerticalMenuSystemItem
    {
        [Parameter]
        public VerticalMenuItem Item { get; set; }

        [Parameter]
        public bool IsLoading { get; set; }
        
        [Parameter]
        public EventCallback<VerticalMenuItem> OnClickItem { get; set; }

        [Inject] IMotionUIController Motion { get; set; }
        /// <summary>
        /// Only Executed when URL is NOT Present
        /// </summary>
        /// <returns></returns>
        private async Task Click()
        {
            if (Item.URL != null) return;
            await OnClickItem.InvokeAsync(Item);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

            }
        }

        private async Task ExitToSubMenu() { 
        
        }
    }
}
