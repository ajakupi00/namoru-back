using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }                

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
           if(_context.Set<T>().Count() > 0)
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            return new List<T>();
        }

        public Task<T> GetEntityWithSpec()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> ListAsyncWithSpec()
        {
            throw new NotImplementedException();
        } 
    }
}
