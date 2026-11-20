using GolBet.Entities.Common;

namespace GolBet.Repositories.Common;

public interface IGenericRepository<T> where T : AuditableEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    void Update(T entity);

    Task DeleteAsync(T entity);

    void Remove(T entity);

    Task<bool> SaveChangesAsync();
}
