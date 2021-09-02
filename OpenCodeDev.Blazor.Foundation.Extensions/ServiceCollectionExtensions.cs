using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OpenCodeDev.Blazor.Foundation.Extensions.Clipboard;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBFClipboard(this IServiceCollection service)
        {
            service.AddSingleton<IClipboard, Clipboard.Clipboard>();
        }
    }
}
