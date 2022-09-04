using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.General
{
    public partial class AppWrapper : ComponentBase, IDisposable
    {
        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        public IStyleManagement? StyleManager { get; set; }

        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            StyleManager = ServiceProvider.GetService<IStyleManagement>();
        }

        public void Dispose()
        {
            
        }
    }
}
