using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetPcApi.Utils.Enums;

namespace NetPcApi.Models
{
    [Table("Contacts")]
    [Index(nameof(Email), IsUnique = true)]
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Category Category { get; set; }
        public string? SubCategory { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}