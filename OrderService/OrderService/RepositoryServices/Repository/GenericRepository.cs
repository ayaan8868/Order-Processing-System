using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.RepositoryServices.Interface;

namespace OrderService.RepositoryServices.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly OrderProcessingSystemDbContext _dbContext;

        protected GenericRepository(OrderProcessingSystemDbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> GetById(Guid id) => await _dbContext.Set<T>().FindAsync(id);

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

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task Save()
        {
          await _dbContext.SaveChangesAsync();
        }
    }
}
