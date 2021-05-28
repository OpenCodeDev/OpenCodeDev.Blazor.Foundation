using System;
using System.Collections.Generic;
using System.Text;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
namespace Blazor_Foundation_6_Test_Shared.Plugins.CSS
{
    public class StyleManagement : BFStyleManagement
    {
        public StyleManagement() : base()
        {
            Set("--doc-menu-link-active-ft-color", "var(--primary-color)", "hide");
            Set("--doc-menu-link-ft-color", "var(--black-color)", "hide");
        }


    }
}
