using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.ValidatorAttributes
{
    public enum PasswordStrength
    {
        /// <summary>
        /// 5 Characters Minimum, 1 Lowercase and 1 Uppercase (Not Recommended)
        /// </summary>
        AllowWeakPassword,
        /// <summary>
        /// 10 Characters Minimum, 1 Lowercase, 1 Uppercase and 1 Digit (Decent Low Security)
        /// </summary>
        AllowDecentPassword,
        /// <summary>
        /// 12 Characters Minimum, 1 Lowercase, 1 Uppercase, 1 Digit and 1 Special (Recommend Strong)
        /// </summary>
        AllowStrongPassword
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordStrengthAttribute : ValidationAttribute
    {
        private int MinSpecialChars { get; set; }
        private int MinDigits { get; set; }
        private int MinLowercaseChars { get; set; }
        private int MinUppercaseChars { get; set; }
        private int MinLength { get; set; }

        public PasswordStrengthAttribute(int minLength, int minSpecialChars, int minDigit, int minLowercaseChars, int minUppercaseChars)
        {
            MinSpecialChars = minSpecialChars;
            MinDigits = minDigit;
            MinLowercaseChars = minLowercaseChars;
            MinUppercaseChars = minUppercaseChars;
            MinLength = minLength;
        }

        public PasswordStrengthAttribute(PasswordStrength strength = PasswordStrength.AllowStrongPassword)
        {
            switch (strength)
            {
                case PasswordStrength.AllowWeakPassword:
                    SetWeakSettings();
                    break;
                case PasswordStrength.AllowDecentPassword:
                    SetDecentSettings();
                    break;
                default:
                    SetStrongSettings();
                    break;
            }
        }

        public override bool IsValid(object value)
        {
            if (value.GetType() == typeof(string)) return false;
            string pwd = (string)value;
            if (!pwd.ContainAllowedOnly()) return false;
            if (pwd.Length < MinLength) return false;
            if (!pwd.ContainSpecial(MinSpecialChars)) return false;
            if (!pwd.ContainDigit(MinDigits)) return false;
            if (!pwd.ContainLowerCase(MinLowercaseChars)) return false;
            if (!pwd.ContainUpperCase(MinUppercaseChars)) return false;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        private void SetWeakSettings()
        {
            MinSpecialChars = 0;
            MinDigits = 0;
            MinLowercaseChars = 1;
            MinUppercaseChars = 1;
            MinLength = 5;
        }

        private void SetDecentSettings()
        {
            MinSpecialChars = 0;
            MinDigits = 1;
            MinLowercaseChars = 1;
            MinUppercaseChars = 1;
            MinLength = 10;
        }

        private void SetStrongSettings()
        {
            MinSpecialChars = 1;
            MinDigits = 1;
            MinLowercaseChars = 1;
            MinUppercaseChars = 1;
            MinLength = 12;
        }
    }
}
