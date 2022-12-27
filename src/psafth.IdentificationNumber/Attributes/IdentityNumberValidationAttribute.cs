using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdentificationNumber.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class IdentificationNumberValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            // TODO: Add validation logic here.
            return result;
        }
    }
}
