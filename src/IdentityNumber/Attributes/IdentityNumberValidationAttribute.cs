using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdentityNumber.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class IdentityNumberValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            // Add validation logic here.
            return result;
        }
    }
}
