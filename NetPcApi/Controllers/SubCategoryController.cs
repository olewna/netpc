using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetPcApi.Dtos.SubCategory;
using NetPcApi.Interfaces;
using NetPcApi.Mappers;

namespace NetPcApi.Controllers
{
    [Route("api/subcategory")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _subcategoryRepository;
        public SubCategoryController(ISubCategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategories = await _subcategoryRepository.GetAllAsync();
            var subCategoriesDtos = subCategories.Select(x => x.ToDtoFromSubCategory());
            return Ok(subCategoriesDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategory = await _subcategoryRepository.GetByIdAsync(id);

            if (subCategory == null)
            {
                return NotFound(new { message = "Nie znaleziono takiej podkategorii" });
            }
            return Ok(subCategory.ToDtoFromSubCategory());
        }

        // [HttpPost("{contactId:int}")]
        // public async Task<IActionResult> Create([FromBody] CreateSubCategoryRequestDto dto, [FromRoute] int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }



        //     var subCategory = dto.();
        //     await _contactRepository.CreateAsync(contact);
        //     return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        // }
    }
}