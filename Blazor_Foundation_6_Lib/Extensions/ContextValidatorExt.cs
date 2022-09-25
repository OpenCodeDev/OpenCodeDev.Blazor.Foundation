using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class ContextValidatorExt
    {

        public static ValidationContext GetContext(this object obj)
        {
            var ctx = new ValidationContext(obj);
            return ctx;
        }

        public static string GetPropErrors(this ValidationContext ctx, string prop) 
        {
            try
            {
                ICollection<ValidationResult> validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(ctx.ObjectInstance, ctx, validationResults, true);
                bool isValid = !validationResults.Any(p=>p.MemberNames.Contains(prop));
                if (isValid) return null;
                return String.Join("\n", validationResults.Where(p => p.MemberNames.Contains(prop)).Select(p => p.ErrorMessage));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetContextErrors(this ValidationContext ctx)
        {
            try
            {
                ICollection<ValidationResult> validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(ctx.ObjectInstance, ctx, validationResults, true);
                if (isValid) return null;
                return String.Join("\n", validationResults.Select(p => p.ErrorMessage));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool IsValid(this ValidationContext ctx)
        {
            try
            {
                return Validator.TryValidateObject(ctx.ObjectInstance, ctx, null, true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Validate Property of a context
        /// </summary>
        public static bool IsPropValid(this ValidationContext ctx, object prop)
        {
            try
            {
                return Validator.TryValidateProperty(prop, ctx, null);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
