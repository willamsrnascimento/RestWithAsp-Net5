using RestWithAspNet.Model.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Repository.BaseRepository
{
    public interface IBaseRepository <T> where T : BaseEntity
    {
        Task<T> CreateAsync(T obj);
        Task<T> FindByIdAsync(long id);
        Task<List<T>> FindAllAsync();
        Task<T> UpdateAsync(T obj);
        Task DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
        Task<List<T>> FindWithPagedSearchAsync(string query);
        int GetCount(string query); 
    }
}
