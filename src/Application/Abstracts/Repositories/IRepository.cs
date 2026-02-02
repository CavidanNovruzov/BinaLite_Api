using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstracts.Repositories;

public interface IRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
{
    Task<List<TEntity>> GetAllAsync(CancellationToken ct=default);
    Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct = default);
    Task AddAsync(TEntity entity, CancellationToken ct = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync(CancellationToken ct = default); 
}
