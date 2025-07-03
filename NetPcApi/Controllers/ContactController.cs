using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetPcApi.Dtos.Contact;
using NetPcApi.Interfaces;
using NetPcApi.Mappers;
using NetPcApi.Models;

namespace NetPcApi.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        public ContactController(IContactRepository contactRepository, ISubCategoryRepository subCategoryRepository)
        {
            _contactRepository = contactRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacts = await _contactRepository.GetAllAsync();
            var dtos = contacts.Select(c => new ContactWithNamesDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Password = c.Password,
                CategoryName = c.Category?.Name,
                SubCategoryName = c.SubCategory?.Name,
                PhoneNumber = c.PhoneNumber,
                DateOfBirth = c.DateOfBirth,
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact == null)
            {
                return NotFound(new { message = "Nie znaleziono tego kontaktu" });
            }

            var dto = new ContactWithNamesDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                CategoryName = contact.Category.Name,
                SubCategoryName = contact.SubCategory?.Name,
                PhoneNumber = contact.PhoneNumber,
                DateOfBirth = contact.DateOfBirth,
            };

            return Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateContactRequestDto contactModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _contactRepository.CheckIfEmailExists(contactModel.Email))
            {
                return Conflict(new { message = "Kontakt z podanym emailem już istnieje" });
            }

            Contact newContact;

            if (!string.IsNullOrWhiteSpace(contactModel.SubCategoryName))
            {
                if (await _subCategoryRepository.CheckIfSubcategoryExists(contactModel.SubCategoryName))
                {

                    var subcategory = await _subCategoryRepository.GetByNameAsync(contactModel.SubCategoryName);

                    newContact = contactModel.ToContactFromDto(subcategory.Id);
                }
                else
                {
                    var newSubcategory = new SubCategory
                    {
                        Name = contactModel.SubCategoryName,
                    };

                    await _subCategoryRepository.CreateAsync(newSubcategory);
                    newContact = contactModel.ToContactFromDto(newSubcategory.Id);
                }
            }
            else
            {
                newContact = contactModel.ToContactFromDto(null);
            }

            // var contact = contactModel.ToContactFromDto();
            await _contactRepository.CreateAsync(newContact);



            var contactDto = new ContactDto
            {
                FirstName = newContact.FirstName,
                LastName = newContact.LastName,
                Email = newContact.Email,
                Password = newContact.Password,
                CategoryId = newContact.CategoryId,
                SubCategoryId = newContact.SubCategoryId,
                PhoneNumber = newContact.PhoneNumber,
                DateOfBirth = newContact.DateOfBirth,
            };

            return CreatedAtAction(nameof(GetById), new { id = newContact.Id }, contactDto);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _contactRepository.DeleteAsync(id);
            if (response == null)
            {
                return NotFound(new { message = "Nie znaleziono kontaktu do usunięcia" });
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactRequestDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundContact = await _contactRepository.GetByIdAsync(id);

            if (foundContact == null)
            {
                return NotFound(new { message = "Nie znaleziono kontaktu" });
            }
            if (!(foundContact.Email == contactDto.Email))
            {
                if (await _contactRepository.CheckIfEmailExists(contactDto.Email))
                {
                    return Conflict(new { message = "Kontakt z podanym emailem już istnieje" });
                }
            }

            int? newSubcategoryId = null;

            if (!string.IsNullOrWhiteSpace(contactDto.SubCategoryName)) // czy podkategoria to nie jest pusty string
            {
                if (await _subCategoryRepository.CheckIfSubcategoryExists(contactDto.SubCategoryName)) //sprawdzamy czy podany string istnieje jako podkategoria
                {
                    var subcategory = await _subCategoryRepository.GetByNameAsync(contactDto.SubCategoryName); // pobieramy podkategorie po nazwie

                    newSubcategoryId = subcategory.Id;
                }
                else
                {
                    var newSubcategory = new SubCategory
                    {
                        Name = contactDto.SubCategoryName,
                    };

                    await _subCategoryRepository.CreateAsync(newSubcategory);
                    newSubcategoryId = newSubcategory.Id;
                }
            }

            var newContact = contactDto.ToContactDtoFromUpdateDto(newSubcategoryId);

            var updateContact = await _contactRepository.UpdateAsync(id, newContact);
            return Ok(updateContact);
        }
    }
}