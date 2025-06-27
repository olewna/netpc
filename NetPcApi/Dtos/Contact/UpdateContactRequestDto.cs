using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetPcApi.Utils.Enums;
using NetPcApi.Validation;

namespace NetPcApi.Dtos.Contact
{
    public class UpdateContactRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Imię powinno mieć przynajmniej 2 znaki")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Nazwisko powinno mieć przynajmniej 2 znaki")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$", ErrorMessage = "Hasło musi zawierać 8 znaków, w tym dużą i małą literę, cyfrę i znak specjalny")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Category Category { get; set; }
        // [Required]
        public string? SubCategory { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(DateOfBirthValidation), nameof(DateOfBirthValidation.MustBeInThePast))]
        public DateTime DateOfBirth { get; set; }
    }
}