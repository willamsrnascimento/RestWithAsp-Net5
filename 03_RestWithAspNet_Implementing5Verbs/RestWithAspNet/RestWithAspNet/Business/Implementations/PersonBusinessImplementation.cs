using RestWithAspNet.Data.Converter.Implementations;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Hypermedia.Utils;
using RestWithAspNet.Model;
using RestWithAspNet.Repository.BaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _personRepository;
        private readonly PersonConverter _personConverter;

        public PersonBusinessImplementation(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _personConverter = new PersonConverter();
        }

        public async Task<PersonVO> CreateAsync(PersonVO personVO)
        {
            var personEntity = _personConverter.Parse(personVO);
            personEntity = await _personRepository.CreateAsync(personEntity);

            return _personConverter.Parse(personEntity);
        }

        public async Task<PersonVO> DisableAsync(long id)
        {
            var personEntity = await _personRepository.DisableAsync(id);
            return _personConverter.Parse(personEntity);
        }

        public async Task DeleteAsync(long id)
        {
            await _personRepository.DeleteAsync(id);
        }

        public async Task<List<PersonVO>> FindAllAsync()
        {
            return _personConverter.Parse(await _personRepository.FindAllAsync());
        }

        public async Task<PagedSearchVO<PersonVO>> FindWithPagedSearchAsync(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT
                                *
                             FROM person p
                             WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query + $"AND p.first_name LIKE '%{name}%' ";
            }

            query += $"ORDER BY p.first_name {sort} " +
                     $"LIMIT {size} " +
                     $"OFFSET {offset}";

            string queryCount = @"SELECT
                                  COUNT(*)
                                  FROM person p 
                                  WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryCount = queryCount + $"AND p.first_name LIKE '%{name}%'";
            }

            var persons = await _personRepository.FindWithPagedSearchAsync(query);
            int totalResults = _personRepository.GetCount(queryCount);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _personConverter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public async Task<PersonVO> FindByIdAsync(long id)
        {
            return _personConverter.Parse(await _personRepository.FindByIdAsync(id));
        }

        public async Task<List<PersonVO>> FindByNameAsync(string firstName, string lastName)
        {
            var personList = await _personRepository.FindByNameAsync(firstName, lastName);
            return _personConverter.Parse(personList);
        }

        public async Task<PersonVO> UpdateAsync(PersonVO personVO)
        {
            var personEntity = _personConverter.Parse(personVO);
            personEntity = await _personRepository.UpdateAsync(personEntity);

            return _personConverter.Parse(personEntity);
        }

    }
}
