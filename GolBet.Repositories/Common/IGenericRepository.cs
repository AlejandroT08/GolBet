using GolBet.Entities.Common;

namespace GolBet.Repositories.Common;

public interface IGenericRepository<T> where T : AuditableEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    void Update(T entity);

    void Remove(T entity);

    Task<bool> SaveChangesAsync();
}
