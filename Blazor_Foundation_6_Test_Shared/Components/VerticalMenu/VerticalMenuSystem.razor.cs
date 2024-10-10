using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Components.VerticalMenu
{
    public partial class VerticalMenuSystem: ComponentBase
    {

        /// <summary>
        /// Main Level Breadcrumb name (Default: Main)
        /// </summary>
        [Parameter]
        public string MainLevelName { get; set; } = "Main";

        [Parameter]
        public List<VerticalMenuItem> Items { get; set; } = new List<VerticalMenuItem>();

        [Parameter]
        public bool IsLoading { get; set; }

        /// <summary>
        /// Vertical Menu Level which is the identifier defining on which menu should the system show
        /// </summary>
        [SupplyParameterFromQuery]
        private string VML { get; set; }

        [Inject] private IMotionUIController Motion { get; set; }

        /// <summary>
        /// Only used when navigating submenus
        /// </summary>
        private Stack<VerticalMenuItem> SubMenuLevel { get; set; } = new Stack<VerticalMenuItem>();
        private bool IsBusy { get; set; } = true;
        private void SetBusy(bool val = true)
        {
            IsBusy = val;
            StateHasChanged();
        }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ShowMenu(Items);
            }
        }


        private async Task OnElementClick(VerticalMenuItem item)
        {
            SetBusy();
            if(SubMenuLevel.Count > 0) await HideMenu(SubMenuLevel.Peek().SubMenus);
            else await HideMenu(Items);
            SubMenuLevel.Push(item);
            SetBusy(false);
        }

        private async Task HideMenu(List<VerticalMenuItem> items)
        {
            SetBusy();
            for (int i = items.Count - 1; i > 0 ; i--)
            {
                Motion.AnimateAndHide($"vmenu{Items[i].Checksum}", "fade-out");
                await Task.Delay(300);
            }
            await Task.Delay(100);
            SetBusy(false);
        }

        private async Task ShowMenu(List<VerticalMenuItem> items)
        {
            SetBusy();
            foreach (var item in items)
            {
                Motion.AnimateAndShow($"vmenu{item.Checksum}", "fade-in");
                await Task.Delay(300);

            }
            await Task.Delay(100);
            SetBusy(false);
        }

    }
}
