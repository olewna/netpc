using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetPcApi.Models;

namespace NetPcApi.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact contactModel);
        // Task<Contact?> UpdateAsync(int id, UpdateContactRequestDto contactDto);
        // Task<Contact?> DeleteAsync(int id);
        Task<bool> CheckIfEmailExists(string email);
    }
}