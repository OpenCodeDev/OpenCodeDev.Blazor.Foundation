using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.ValidatorAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AbideGuidAttribute : ValidationAttribute
    {
        private bool AllowEmpty { get; set; }
        public AbideGuidAttribute(bool allowEmpty = false)
        {
            AllowEmpty = allowEmpty;
        }
        public override bool IsValid(object value)
        {
            bool isNullable = value.GetType() == typeof(Guid?);
            if (!isNullable  && value == null) return false;
            Guid valueCon = (Guid)value;
            if (!AllowEmpty && valueCon == Guid.Empty) return false;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}
