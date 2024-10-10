using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Components.VerticalMenu
{
    public partial class VerticalMenuSystem: ComponentBase
    {

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
        /// <summary>
        /// Back Button(Default: Back)
        /// </summary>
        [Parameter]
        public VerticalMenuItem BackButtonItem { get; set; } = new VerticalMenuItem() { Behavior = VerticalMenuItemBehavior.SubMenuNavigate, Name = "Back", ShortDesc = "Back to previous menu.", IconHTML = "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 72 72\" enable-background=\"new 0 0 72 72\" xml:space=\"preserve\"><g id=\"SVGRepo_bgCarrier\" stroke-width=\"0\"></g><g id=\"SVGRepo_tracerCarrier\" stroke-linecap=\"round\" stroke-linejoin=\"round\"></g><g id=\"SVGRepo_iconCarrier\"> <g> <path d=\"M48.252,69.253c-2.271,0-4.405-0.884-6.011-2.489L17.736,42.258c-1.646-1.645-2.546-3.921-2.479-6.255 c-0.068-2.337,0.833-4.614,2.479-6.261L42.242,5.236c1.605-1.605,3.739-2.489,6.01-2.489c2.271,0,4.405,0.884,6.01,2.489 c3.314,3.314,3.314,8.707,0,12.021L35.519,36l18.743,18.742c3.314,3.314,3.314,8.707,0,12.021 C52.656,68.369,50.522,69.253,48.252,69.253z M48.252,6.747c-1.202,0-2.332,0.468-3.182,1.317L21.038,32.57 c-0.891,0.893-0.833,2.084-0.833,3.355c0,0.051,0,0.101,0,0.151c0,1.271-0.058,2.461,0.833,3.353l24.269,24.506 c0.85,0.85,1.862,1.317,3.063,1.317c1.203,0,2.273-0.468,3.123-1.317c1.755-1.755,1.725-4.61-0.03-6.365L31.292,37.414 c-0.781-0.781-0.788-2.047-0.007-2.828L51.438,14.43c1.754-1.755,1.753-4.61-0.001-6.365C50.587,7.215,49.454,6.747,48.252,6.747z\"></path> </g> </g></svg>" };
        private bool IsBusy { get; set; } = true;
        private void SetBusy(bool val = true)
        {
            IsBusy = val;
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
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
            if (item.Behavior != VerticalMenuItemBehavior.SubMenuNavigate) return;
            if (item.SubMenus.Count <= 0 && item != BackButtonItem) return; // cancel if nothing to navigate
            SetBusy();
            if(SubMenuLevel.Count > 0) await HideMenu(SubMenuLevel.Peek().SubMenus);
            else await HideMenu(Items);

            if (BackButtonItem == item) SubMenuLevel.Pop();
            else SubMenuLevel.Push(item);
            SetBusy(false);
            await Task.Delay(50);
            if (SubMenuLevel.Count > 0)
                await ShowMenu(SubMenuLevel.Peek().SubMenus);
            else await ShowMenu(Items);
        }

        private async Task HideMenu(List<VerticalMenuItem> items)
        {
            SetBusy();
            if (SubMenuLevel.Count > 0) {
                Motion.AnimateAndHide($"vmenu{BackButtonItem.Checksum}", "fade-out");
                await Task.Delay(100);
            }

            for (int i = items.Count - 1; i > 0 ; i--)
            {
                Motion.AnimateAndHide($"vmenu{Items[i].Checksum}", "fade-out");
                await Task.Delay(100);
            }
            await Task.Delay(100);
            SetBusy(false);
        }

        private async Task ShowMenu(List<VerticalMenuItem> items)
        {
            SetBusy();
            if (SubMenuLevel.Count > 0)
            {
                Motion.AnimateAndShow($"vmenu{BackButtonItem.Checksum}", "fade-in");
                await Task.Delay(100);
            }
            foreach (var item in items)
            {
                Motion.AnimateAndShow($"vmenu{item.Checksum}", "fade-in");
                await Task.Delay(100);

            }
            await Task.Delay(100);
            SetBusy(false);
        }

    }
}
