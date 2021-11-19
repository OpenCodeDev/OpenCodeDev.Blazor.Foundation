using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.LocalStorage
{
    public class LocalStorage : ILocalStorage{ 
        private IJSRuntime _runtime { get; set; }

        public LocalStorage (IJSRuntime runtime){
            _runtime = runtime;
        }

        public async Task<string> Get(string key)
        {
            return await _runtime.InvokeAsync<string>("LocalStorageGet", key);
        }

        public async Task Set(string key, string value)
        {
            await _runtime.InvokeVoidAsync("LocalStorageSet", key, value);
        }

        public async Task Remove(string key){
            await _runtime.InvokeVoidAsync("LocalStorageRemove", key);
        }

        public async Task Clear(){
            await _runtime.InvokeVoidAsync("LocalStorageClear");
        }

    }
}
