using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetPcApi.Dtos.SubCategory;
using NetPcApi.Models;

namespace NetPcApi.Mappers
{
    public static class SubCategoryMapper
    {
        public static SubCategoryDto ToDtoFromSubCategory(this SubCategory model)
        {
            return new SubCategoryDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }
        // public static SubCategory ToDtoFromSubCategory(this SubCategoryDto dto)
        // {
        //     return new SubCategory
        //     {
        //         Id = model.Id,
        //         Name = model.Name
        //     };
        // }
    }
}