using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Business
{
    public interface IBookBusiness
    {
        Task<BookVO> CreateAsync(BookVO bookVO);
        Task<BookVO> FindByIdAsync(long id);
        Task<List<BookVO>> FindAllAsync();
        Task<BookVO> UpdateAsync(BookVO bookVO);
        Task DeleteAsync(long id);

    }
}
