using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.InfiniteScrollHelper;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.MotionUI;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.Extensions.DependencyInjection;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Foundation;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Reveal;
using OpenCodeDev.Blazor.Foundation.Extensions.Clipboard;
using OpenCodeDev.Blazor.Foundation.Extensions.LocalStorage;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine;
using System.Reflection;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown;

namespace OpenCodeDev.Blazor.Foundation
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// InifiteLoadHelper, MotionUI and StyleManagement.
        /// </summary>
        [Obsolete("This function is deprecated as of v3.1, any new feature added 3.1+ may not be available if using this. You can use individual method or AddBlazorFoundationServices()")]
        public static void AddAllBFPlugins(this IServiceCollection service) {
            service.AddFoundationController();
            service.AddBFInfiniteLoadHelper();
            service.AddBFMotionUI();
            service.AddBFStyleManagement();
        }

        public static void AddBlazorFoundationServices(this IServiceCollection service, bool isWasm = true) => AllPlugins(service, isWasm);

        private static void AllPlugins(IServiceCollection service, bool isWasm = true)
        {
            service.AddFoundationController();
            service.AddBFInfiniteLoadHelper();
            service.AddBFMotionUI();
            service.AddBFStyleManagement();
            service.AddNovelRevealController(isWasm);
            service.AddBFClipboard(isWasm);
            service.AddBFLocalStorage(isWasm);
        }
        public static void AddNovelRevealController(this IServiceCollection service, bool isWasm = true)
        {
            if (!isWasm)
            {
                service.AddScoped<INovelRevealController, NovelRevealController>(p =>
                    { return new NovelRevealController(p.GetRequiredService<IJSRuntime>()); 
                });
            }
            else
            {
                service.AddSingleton<INovelRevealController, NovelRevealController>(p =>
                {
                    return new NovelRevealController(p.GetRequiredService<IJSRuntime>());
                });
            }
        }

        public static void AddBFClipboard(this IServiceCollection service, bool isWasm = true)
        {
            if (!isWasm)
            {
                service.AddScoped<IClipboard, Clipboard>(p =>
                {
                    return new Clipboard(p.GetRequiredService<IJSRuntime>());
                });
            }
            else
            {
                service.AddSingleton<IClipboard, Clipboard>(p =>
                {
                    return new Clipboard(p.GetRequiredService<IJSRuntime>());
                });
            }
        }

        public static void AddBFLocalStorage(this IServiceCollection service, bool isWasm = true)
        {
            if (!isWasm)
            {
                service.AddScoped<ILocalStorage, LocalStorage>(p =>
                {
                    return new LocalStorage(p.GetRequiredService<IJSRuntime>());
                });
            }
            else
            {
                service.AddSingleton<ILocalStorage, LocalStorage>(p =>
                {
                    return new LocalStorage(p.GetRequiredService<IJSRuntime>());
                });
            }
        }

        public static void AddFoundationController(this IServiceCollection service)
        {
            service.AddScoped<IFoundationElementController, FoundationElementController>(p =>
            {
                return new FoundationElementController(p.GetRequiredService<IJSRuntime>());
            });
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


        public static void AddMarkdownSystem(this IServiceCollection service) {
            // TODO: Make sure to get the ones in loaded plugins
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(p=>p.GetTypes());
            var methods = allTypes.SelectMany(t => t.GetMethods())
                      .Where(p => p.IsStatic && p.IsPublic)
                      .Where(m => m.GetCustomAttributes(typeof(RegisterMarkdownAttribute), true).Length > 0)
                      .ToArray();

            // All Methods 
            foreach (var meth in methods)
            {
                int paramsAmount = meth.GetParameters().Length;
                if (paramsAmount != 1) continue; // invalid ignore.

                foreach (RegisterMarkdownAttribute attr in meth.GetCustomAttributes(typeof(RegisterMarkdownAttribute), true)) {
                    string? fullname = meth.ReflectedType?.FullName;
                    if (fullname == null) throw new Exception("Cannot get full name by reflection for some reasons..");
                    string concatName = $"{fullname}.{meth.Name}";
                    // Convert method info to Task.
                    try {
                        Func<MarkdownComponent, Task<MarkdownElement?>> delegateMethod = (Func<MarkdownComponent, Task<MarkdownElement?>>)meth.CreateDelegate(typeof(Func<MarkdownComponent, Task<MarkdownElement?>>));
                        MarkdownSystem.RegisterComponent(attr.Name, delegateMethod);
                    } catch (Exception ex) {
                        // TODO: Log Error and Move on
                        throw;
                    }
                }
            }
        }
    }
}
