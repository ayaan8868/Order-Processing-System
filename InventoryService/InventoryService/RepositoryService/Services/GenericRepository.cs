using InventoryService.Context;
using InventoryService.RepositoryService.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.RepositoryService.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly InvernotoryDbContext _dbContext;

        protected GenericRepository(InvernotoryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
