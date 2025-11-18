using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFramework.v2
{
    public interface IPartRepository
    {
        Task<List<Part>> GetAllAsync();
        Task<Part> GetByIdAsync(int id);
        Task AddAsync(Part part);
        Task UpdateAsync(Part part);
        Task DeleteAsync(int id);
    }
}
