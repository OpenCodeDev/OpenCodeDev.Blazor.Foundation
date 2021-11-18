using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using OpenCodeDev.Blazor.Foundation.Extensions.Clipboard;
using OpenCodeDev.Blazor.Foundation.Extensions.LocalStorage;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBFClipboard(this IServiceCollection service)
        {
            service.AddSingleton<IClipboard, Clipboard.Clipboard>();
        }

        public static void AddBFLocalStorage(this IServiceCollection service)
        {
            service.AddSingleton<ILocalStorage, LocalStorage.LocalStorage>();
        }
    }
}
