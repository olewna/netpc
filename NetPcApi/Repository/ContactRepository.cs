using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetPcApi.Data;
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

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contact.ToListAsync();
            // TODO dodać paginacje
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contact.FindAsync(id);
        }
    }
}