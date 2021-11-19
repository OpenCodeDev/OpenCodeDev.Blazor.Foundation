using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenCodeDev.Blazor.Foundation.Extensions.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task<WebAssemblyHost> LoadCulture(this WebAssemblyHost wasm, string defaultLang = "en")
        {
            var localStorage = wasm.Services.GetRequiredService<ILocalStorage>();
            var savedCulture = await localStorage.Get("culture");
            if (savedCulture != null) {
                var currentLang = new CultureInfo(savedCulture);
                CultureInfo.DefaultThreadCurrentCulture = currentLang;
                CultureInfo.DefaultThreadCurrentUICulture = currentLang;

                await localStorage.Set("isFirstRun", "false");
            }
            else { 
                await localStorage.Set("culture", defaultLang); 
                await localStorage.Set("isFirstRun", "true");
            }
            return wasm;
        }
    }
}
