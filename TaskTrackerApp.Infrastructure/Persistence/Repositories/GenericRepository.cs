using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.Domain.Common;

namespace TaskTrackerApp.Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(ApplicationDbContext appDbContext) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> _dbSet = appDbContext.Set<T>();

    public virtual IQueryable<T> GetAll() => _dbSet.AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();

    public ValueTask<T?> GetByIdAsync(Guid id) => _dbSet.FindAsync(id);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        => _dbSet.AnyAsync(expression);

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}
