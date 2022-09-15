using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenCodeDev.Blazor.Foundation.Extensions.ValidatorAttributes
{
    /// <summary>
    /// Used to define an array
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AbideUrlAttribute : ValidationAttribute
    {
        public bool AcceptsNull { get; set; } = false;
        public AbideUrlAttribute(bool acceptsNull = false)
        {
            AcceptsNull = acceptsNull;
        }

        public override bool IsValid(object value)
        {
            if (value == null && !AcceptsNull) { return false; }
            var regex = new Regex(Abide.URL);
            string valueCon = (string)value;
            return regex.IsMatch(valueCon);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

    }
}
