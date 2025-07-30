using System.Linq.Expressions;
using TaskTrackerApp.Domain.Common;

namespace TaskTrackerApp.Application.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    ValueTask<T?> GetByIdAsync(Guid id);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

    ValueTask AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}
