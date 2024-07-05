using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Common
{
    public static class ValidationTime
    {
        public static ValidationResult ValidateExpirationDate(DateTime ExpOn, ValidationContext context)
        {
            if (ExpOn <= DateTime.Now)
            {
                return new ValidationResult("Expiration date must be in the future.");
            }

            return ValidationResult.Success;
        }
    }
}
