using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetPcApi.Data;
using NetPcApi.Dtos.Contact;
using NetPcApi.Interfaces;
using NetPcApi.Models;

namespace NetPcApi.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDBContext _context;
        public ContactRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await _context.Contact.AnyAsync(contact => contact.Email == email);
        }

        public async Task<Contact> CreateAsync(Contact contactModel)
        {
            await _context.Contact.AddAsync(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<Contact?> DeleteAsync(int id)
        {
            var foundContact = await _context.Contact.FindAsync(id);

            if (foundContact == null)
            {
                return null;
            }

            _context.Contact.Remove(foundContact);
            await _context.SaveChangesAsync();
            return foundContact;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contact.ToListAsync();
            // TODO dodać paginacje
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contact.FindAsync(id);
        }

        public async Task<Contact?> UpdateAsync(int id, UpdateContactRequestDto contactDto)
        {
            var foundContact = await _context.Contact.FindAsync(id);
            if (foundContact == null)
            {
                return null;
            }

            _context.Entry(foundContact).CurrentValues.SetValues(contactDto);
            await _context.SaveChangesAsync();
            return foundContact;
        }
    }
}