using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Clipboard;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.InfiniteScrollHelper;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.Extensions.DependencyInjection;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins
{
    public static class ServiceCollectionExtensions
    {
        
        public static void AddAllBFPlugins(this IServiceCollection service)
        {
            service.AddBFInfiniteLoadHelper();
            service.AddBFMotionUI();
            service.AddBFClipboard();
            service.AddBFStyleManagement();

        }

        public static void AddBFStyleManagement(this IServiceCollection service)
        {
            service.AddSingleton<IStyleManagement, BFStyleManagement>();
        }

        public static void RemoveBFStyleManagement(this IServiceCollection service)
        {
            
            var serviceDescriptor = service.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IStyleManagement));
            service.Remove(serviceDescriptor);
        }


        public static void AddBFClipboard(this IServiceCollection service)
        {
            service.AddSingleton<IClipboard, Clipboard.Clipboard>();
        }

        public static void AddBFInfiniteLoadHelper(this IServiceCollection service)
        {
            service.AddSingleton<IInfiniteLoadHelper, InfiniteLoadHelper>();
        }
        public static void AddBFMotionUI(this IServiceCollection service)
        {
            service.AddTransient<IMotionUIController, MotionUIController>();
        }
    }
}
