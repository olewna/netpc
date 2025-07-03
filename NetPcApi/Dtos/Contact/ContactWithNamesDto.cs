using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPcApi.Dtos.Contact
{
    public class ContactWithNamesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string? SubCategoryName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}