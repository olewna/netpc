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
            return await _context.Contacts.AnyAsync(contact => contact.Email == email);
        }

        public async Task<Contact> CreateAsync(Contact contactModel)
        {
            await _context.Contacts.AddAsync(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<Contact?> DeleteAsync(int id)
        {
            var foundContact = await _context.Contacts.FindAsync(id);

            if (foundContact == null)
            {
                return null;
            }

            _context.Contacts.Remove(foundContact);
            await _context.SaveChangesAsync();
            return foundContact;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .FirstOrDefaultAsync(contact => contact.Id == id);
        }

        public async Task<Contact?> UpdateAsync(int id, ContactDto contactDto)
        {
            var foundContact = await _context.Contacts.FindAsync(id);
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