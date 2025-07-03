using System.ComponentModel.DataAnnotations;
using NetPcApi.Validation;

namespace NetPcApi.Dtos.Contact
{
    public class CreateContactRequestDto
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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,}$", ErrorMessage = "Hasło musi zawierać 8 znaków, w tym dużą i małą literę, cyfrę i znak specjalny")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
        // [Required]
        // public int? SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(DateOfBirthValidation), nameof(DateOfBirthValidation.MustBeInThePast))]
        public DateTime DateOfBirth { get; set; }
    }
}