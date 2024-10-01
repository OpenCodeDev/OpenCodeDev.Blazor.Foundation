using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Extensions;
namespace OpenCodeDev.Blazor.Foundation.Components.Plugins.Markdown.Engine
{
    /// <summary>
    /// Register a Mardown Processor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RegisterMarkdownAttribute : Attribute {
        /// <summary>
        /// Name of the component.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Name of the component.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Full namespace
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Checksum of the path
        /// </summary>
        public string Checksum { get; private set; }

        /// <summary>
        /// Type of Component
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Internal Component Support which means the path to use it is directly YOURCOMPONENT instead of Namespace.YourComponent very dangerous prone to conflict. (NOT RECOMMENDED)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="internalBool"></param>
        public RegisterMarkdownAttribute(Type type, bool internalBool) : this(type) {
            Id = Name;
        }

        /// <summary>
        /// Register a Markdown Parser
        /// </summary>
        /// <param name="name">Name of the component (Prefix recommended for any published packages)</param>
        public RegisterMarkdownAttribute(Type type)
        {
            Name = type.Name;
            Path = type.Namespace;
            Checksum = type.FullName.ToCRC32();
            Id = Checksum + "." + Name;
            Type = type;
        }



    }
}
