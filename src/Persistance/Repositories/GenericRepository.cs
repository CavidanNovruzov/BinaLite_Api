using Application.Abstracts.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories;

public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly BinaLiteDbContext _context;
    private readonly DbSet<TEntity> _table;
    public GenericRepository(BinaLiteDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }
    public async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _table.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct = default)
    {
        return await _table.FindAsync(id);

    }

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _table.AddAsync(entity);
    }
    public void Update(TEntity entity)
    {
        _table.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _table.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync();
    }


}
