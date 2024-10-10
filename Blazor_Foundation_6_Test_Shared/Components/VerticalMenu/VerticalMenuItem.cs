using Microsoft.AspNetCore.Components;
using OpenCodeDev.Blazor.Foundation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Components.VerticalMenu
{
    public enum VerticalMenuItemBehavior
    {
        /// <summary>
        /// Sub Menu will go to defined URL which should contain component VeticalSubMenuPage. <br/>
        /// The system will pass in query MenuId=1,1,1 as string to fetch the navigation.
        /// </summary>
        SubMenuPage = 0,

        /// <summary>
        /// Element of the current side vertical menu will be replaced by the sub menu's
        /// </summary>
        SubMenuNavigate = 1,
    }

    /// <summary>
    /// Define Item for Vertical Menu.
    /// </summary>
    public class VerticalMenuItem {

        /// <summary>
        /// Name of the Menu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short description of the menu
        /// </summary>
        public string ShortDesc { get; set; } 


        /// <summary>
        /// This option is disable when submenu contains element and .
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Icon as a string html markup and ignored if IconFragment defined.
        /// </summary>
        public string IconHTML { get; set; }

        /// <summary>
        /// Icon as RenderFragment
        /// </summary>
        public RenderFragment IconFragment { get; set; }

        /// <summary>
        /// Add submenu elements
        /// </summary>
        public List<VerticalMenuItem> SubMenus { get; set; } = new List<VerticalMenuItem>();
        
        /// <summary>
        /// Define menu behavior which 
        /// </summary>
        public VerticalMenuItemBehavior Behavior { get; set; } = VerticalMenuItemBehavior.SubMenuPage;

        /// <summary>
        /// This is used as unique identifier of the menu.
        /// </summary>
        public string Checksum => (Name + ShortDesc + URL + IconHTML).ToCRC64();
    }

}
