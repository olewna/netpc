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
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacts = await _contactRepository.GetAllAsync();
            return Ok(contacts);
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
            return Ok(contact);
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateContactRequestDto contactModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _contactRepository.CheckIfEmailExists(contactModel.Email))
            {
                return Conflict(new { message = "A contact with this email already exists." });
            }

            var contact = contactModel.ToContactFromDto();
            await _contactRepository.CreateAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }
    }
}