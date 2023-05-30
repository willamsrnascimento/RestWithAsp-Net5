using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Repository.BaseRepository
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person> DisableAsync(long id);
        Task<List<Person>> FindByNameAsync(string firstName, string lastName);
    }
}
