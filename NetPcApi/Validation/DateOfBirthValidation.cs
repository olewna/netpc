using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetPcApi.Validation
{
    public class DateOfBirthValidation
    {
        public static ValidationResult MustBeInThePast(DateTime date, ValidationContext context)
        {
            if (date >= DateTime.Today)
            {
                return new ValidationResult("Powinieneś urodzić się przed aktualną datą...");
            }

            return ValidationResult.Success;
        }
    }
}