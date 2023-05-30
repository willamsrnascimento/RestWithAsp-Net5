using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Model.Base;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet.Repository.Implementations
{
    public class GenericImplementation<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected MySqlContext _context;
        private DbSet<T> _dataSet;

        public GenericImplementation(MySqlContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }
        public async Task<T> CreateAsync(T obj)
        {
            try
            {
                _dataSet.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return await FindByIdAsync(obj.Id);
        }

        public async Task DeleteAsync(long id)
        {
            var result = await FindByIdAsync(id);

            if (result != null)
            {
                try
                {
                    _dataSet.Remove(result);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await _dataSet.ToListAsync();
        }

        public async Task<T> FindByIdAsync(long id)
        {
            if (!await ExistsAsync(id))
            {
                return null;
            }

            return await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }


        public async Task<T> UpdateAsync(T obj)
        {
            var result = await FindByIdAsync(obj.Id);

            if (result == null)
            {
                return null;
            }

            try
            {
                _context.Entry(result).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }


            return result;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _dataSet.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<List<T>> FindWithPagedSearchAsync(string query)
        {
            return await _dataSet.FromSqlRaw<T>(query).ToListAsync();
        }

        public int GetCount(string query)
        {
            var result = "";

            using(var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return int.Parse(result);
        }
    }
}
