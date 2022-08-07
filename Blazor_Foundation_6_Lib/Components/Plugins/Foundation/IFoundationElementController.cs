using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Foundation
{
    public interface IFoundationElementController
    {
        /// <summary>
        /// Trigger open foundation function for a specific foundation element.
        /// </summary>
        Task Open(string id);

        /// <summary>
        /// Trigger close founation function for a specific foundation element.
        /// </summary>
        Task Close(string id);

        /// <summary>
        /// Trigger toggle founation function for a specific foundation element.
        /// </summary>
        Task Toggle(string id);
    }
}
