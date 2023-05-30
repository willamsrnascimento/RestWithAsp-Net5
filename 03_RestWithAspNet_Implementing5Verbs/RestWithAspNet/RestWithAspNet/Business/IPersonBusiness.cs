using RestWithAspNet.Data.VO;
using RestWithAspNet.Hypermedia.Utils;
using RestWithAspNet.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Business
{
    public interface IPersonBusiness
    {
        Task<PersonVO> CreateAsync(PersonVO personVO);
        Task<PersonVO> FindByIdAsync(long id);
        Task<List<PersonVO>> FindByNameAsync(string firstName, string lastName);
        Task<List<PersonVO>> FindAllAsync();
        Task<PagedSearchVO<PersonVO>> FindWithPagedSearchAsync(string name, string sortDirection, int pageSize, int page);
        Task<PersonVO> UpdateAsync(PersonVO personVO);
        Task<PersonVO> DisableAsync(long id);
        Task DeleteAsync(long id);

    }
}
