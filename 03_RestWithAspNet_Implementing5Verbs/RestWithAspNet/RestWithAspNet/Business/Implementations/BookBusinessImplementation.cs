using RestWithAspNet.Data.Converter.Implementations;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IBaseRepository<Book> _baseRepository;
        private readonly BookConverter _bookConverter;

        public BookBusinessImplementation(IBaseRepository<Book> baseRepository)
        {
            _baseRepository = baseRepository;
            _bookConverter = new BookConverter();
        }

        public async Task<BookVO> CreateAsync(BookVO bookVO)
        {
            var bookEntity = _bookConverter.Parse(bookVO);
            bookEntity = await _baseRepository.CreateAsync(bookEntity);

            return _bookConverter.Parse(bookEntity);
        }

        public async Task DeleteAsync(long id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task<List<BookVO>> FindAllAsync()
        {
            return _bookConverter.Parse(await _baseRepository.FindAllAsync());
        }

        public async Task<BookVO> FindByIdAsync(long id)
        {
            return _bookConverter.Parse(await _baseRepository.FindByIdAsync(id));
        }


        public async Task<BookVO> UpdateAsync(BookVO bookVO)
        {
            var bookEntity = _bookConverter.Parse(bookVO);
            bookEntity = await _baseRepository.UpdateAsync(bookEntity);

            return _bookConverter.Parse(bookEntity);
        }

    }
}
