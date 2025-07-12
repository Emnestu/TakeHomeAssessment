using ContactSystem.Application.Entities;

namespace ContactSystem.Application.Repositories.Interfaces
{
    public interface IEntitiesRepository<TEntity, TKey> where TEntity : Entity<TKey> where TKey : struct
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
