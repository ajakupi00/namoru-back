using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec();
        Task<IReadOnlyList<T>> ListAsyncWithSpec();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
