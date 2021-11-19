using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Set key pair value string string
        /// </summary>
        Task Set(string key, string value);

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
