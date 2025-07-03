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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDBContext _context;
        public SubCategoryRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<SubCategory> CreateAsync(SubCategory model)
        {
            await _context.SubCategories.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory?> GetByIdAsync(int id)
        {
            return await _context.SubCategories.FindAsync(id);
        }
    }
}