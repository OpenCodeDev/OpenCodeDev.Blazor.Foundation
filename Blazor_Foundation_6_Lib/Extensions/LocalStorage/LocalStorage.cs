using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public async Task<T> Get<T>(string key)
        {
            var obj = await _runtime.InvokeAsync<string>("LocalStorageGet", key);
            if (obj == null) return default(T);
            return JsonSerializer.Deserialize<T>(obj);
        }

        public async Task Set<TEntity>(string key, TEntity entity, JsonSerializerOptions options = null)
        {
            string jsonString = JsonSerializer.Serialize(entity, options);
            await _runtime.InvokeVoidAsync("LocalStorageSet", key, jsonString);
        }

        public async Task<bool> Exist(string key)
        {
           var element = await _runtime.InvokeAsync<string>("LocalStorageGet", key);
           return element != null;
        }
    }
}
