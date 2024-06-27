using OpenCodeDev.Blazor.Foundation;
using OpenCodeDev.Blazor.Foundation.Extensions;
using OpenCodeDev.Blazor.Foundation.Doc.Core.Plugins.CSS;
using OpenCodeDev.Blazor.Foundation.Components.Plugins.StyleManager;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Web;

namespace OpenCodeDev.Blazor.Foundation.Doc.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            //builder.RootComponents.Add<App>("app");
            builder.Services.AddLocalization();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // Required to Manage the Theme Color Style Dynamically
            builder.Services.AddBlazorFoundationServices();

            await builder.Build().RunAsync();
        }
    }
}
