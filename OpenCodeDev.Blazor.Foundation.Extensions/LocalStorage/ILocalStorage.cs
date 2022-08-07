using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.LocalStorage
{
    public interface ILocalStorage
    {
        /// <summary>
        /// Get value from key.
        /// </summary>
        Task<string> Get(string key);

        /// <summary>
        /// Get and Auto Deserialize JSON
        /// </summary>
        Task<T> Get<T> (string key);

        /// <summary>
        /// Set key pair value string string
        /// </summary>
        Task Set(string key, string value);

        Task Set<TEntity>(string key, TEntity entity, JsonSerializerOptions options = null);

        Task<bool> Exist(string key);


        /// <summary>
        /// Remove key
        /// </summary>
        Task Remove(string key);

        /// <summary>
        /// Clear all localStorage
        /// </summary>
        Task Clear();
    }
}
