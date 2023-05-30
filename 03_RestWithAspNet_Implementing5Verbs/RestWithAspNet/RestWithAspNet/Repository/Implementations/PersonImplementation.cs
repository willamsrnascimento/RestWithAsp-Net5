using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Repository.Implementations
{
    public class PersonImplementation : GenericImplementation<Person>, IPersonRepository
    {
        public PersonImplementation(MySqlContext context) : base(context)
        {
        }

        public async Task<Person> DisableAsync(long id)
        {
            if(!await _context.Persons.AnyAsync(p => p.Id.Equals(id)))
            {
                return null;
            }

            var user = await _context.Persons.SingleOrDefaultAsync(p => p.Id.Equals(id));

            if(user != null)
            {
                user.Enabled = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return user;
        }

        public async Task<List<Person>> FindByNameAsync(string firstName, string lastName)
        {


            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList(); 
            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return await _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToListAsync();
            }

            return null; 
        }
    }
}
