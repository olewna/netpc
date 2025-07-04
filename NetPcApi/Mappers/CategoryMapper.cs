using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetPcApi.Dtos.Category;
using NetPcApi.Models;

namespace NetPcApi.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDtoFromCategory(this Category model)
        {
            return new CategoryDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}