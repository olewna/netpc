using NetPcApi.Models;

namespace NetPcApi.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAllAsync();
        Task<SubCategory> CreateAsync(SubCategory model);
        Task<SubCategory?> GetByIdAsync(int id);
    }
}