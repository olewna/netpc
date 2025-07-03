using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetPcApi.Models
{
    [Table("SubCategories")]
    [Index(nameof(Name), IsUnique = true)]
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}