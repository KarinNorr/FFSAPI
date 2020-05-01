using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FFSAPI.Contracts;
using FFSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FFSAPI.Repository
{
    //generiskt RepositoryBase som implemeterar interface
    //kan användas mot samtliga klasser 
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly MyDbContext _context;

        public RepositoryBase(MyDbContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
