using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetPcApi.Dtos.Contact;
using NetPcApi.Models;

namespace NetPcApi.Mappers
{
    public static class ContactMapper
    {
        public static Contact ToContactFromDto(this CreateContactRequestDto contactDto)
        {
            return new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Password = contactDto.Password,
                Category = contactDto.Category,
                SubCategory = contactDto.SubCategory,
                PhoneNumber = contactDto.PhoneNumber,
                DateOfBirth = contactDto.DateOfBirth
            };
        }
    }
}