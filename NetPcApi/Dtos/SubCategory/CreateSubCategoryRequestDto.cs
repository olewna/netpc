using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetPcApi.Dtos.SubCategory
{
    public class CreateSubCategoryRequestDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}