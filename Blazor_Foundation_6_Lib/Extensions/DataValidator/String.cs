using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions.DataValidator
{
    public static class String
    {

        public static bool AlphaNumericOnly(this string field)
        {
            if (field.Length <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate not empty
        /// </summary>
        public static bool Required(this string field)
        {
            if (field.Length <= 0)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Validate Email
        /// </summary>
        public static bool ValidateEmail(this string field)
        {
            try
            {
                MailAddress mail = new MailAddress(field);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
