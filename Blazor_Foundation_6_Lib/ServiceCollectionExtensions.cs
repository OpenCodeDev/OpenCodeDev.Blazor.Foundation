using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.InfiniteScrollHelper;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.Extensions.DependencyInjection;
using OpenCodeDev.Blazor.Foundation.Extensions;

namespace OpenCodeDev.Blazor.Foundation
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// InifiteLoadHelper, MotionUI and StyleManagement.
        /// </summary>
        public static void AddAllBFPlugins(this IServiceCollection service)
        {
            service.AddBFInfiniteLoadHelper();
            service.AddBFMotionUI();
            service.AddBFStyleManagement();
        }

        public static void AddBFStyleManagement(this IServiceCollection service)
        {
            service.AddScoped<IStyleManagement, BFStyleManagement>();
        }

        public static void RemoveBFStyleManagement(this IServiceCollection service)
        {
            
            var serviceDescriptor = service.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IStyleManagement));
            service.Remove(serviceDescriptor);
        }


        public static void AddBFInfiniteLoadHelper(this IServiceCollection service)
        {
            service.AddScoped<IInfiniteLoadHelper, InfiniteLoadHelper>();
        }
        public static void AddBFMotionUI(this IServiceCollection service)
        {
            service.AddScoped<IMotionUIController, MotionUIController>();
        }
    }
}
