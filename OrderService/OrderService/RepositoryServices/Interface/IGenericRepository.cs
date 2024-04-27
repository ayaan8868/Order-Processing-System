namespace OrderService.RepositoryServices.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task Save();

    }
}
