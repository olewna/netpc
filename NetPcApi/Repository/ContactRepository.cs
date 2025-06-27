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
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contact.ToListAsync();
            // TODO dodać paginacje
        }
    }
}