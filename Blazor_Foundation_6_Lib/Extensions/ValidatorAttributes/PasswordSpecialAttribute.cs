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
    public class PasswordSpecialAttribute : ValidationAttribute
    {

        public int AtLeast { get; set; } = 1;

        public override bool IsValid(object value)
        {
            string pwd = (string)value;
            return pwd.ContainSpecial(AtLeast);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

    }
}
