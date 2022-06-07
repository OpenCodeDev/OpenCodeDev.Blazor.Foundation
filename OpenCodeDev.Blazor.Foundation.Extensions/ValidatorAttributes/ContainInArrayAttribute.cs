using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
namespace OpenCodeDev.Blazor.Foundation.Extensions.ValidatorAttributes
{
    /// <summary>
    /// Used to define an array
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ContainsInArrayAttribute : ValidationAttribute
    {
        /// <summary>
        /// List of Selectable Options
        /// </summary>
        public object[]? Selectable { get; set; } = null;

        /// <summary>
        /// True: will return validate true if null value is provided.
        /// </summary>
        public bool AcceptsNull { get; set; } = false;

        public override bool IsValid(object value)
        {
            if (Selectable == null) { return false; }
            if (value == null && AcceptsNull) { return true; }
            if (value == null && !AcceptsNull) { return false; }
            return Selectable.Contains(value);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

    }
}
