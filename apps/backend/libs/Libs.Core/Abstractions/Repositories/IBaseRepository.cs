using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Abstractions.Repositories;

public interface IBaseRepository<TPrimaryKeyType, TEntity>
    where TPrimaryKeyType : struct
    where TEntity : BaseEntity<TPrimaryKeyType>
{
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(TPrimaryKeyType id, CancellationToken cancellationToken = default);
    Task DeleteByReferenceAsync(Guid reference, CancellationToken cancellationToken = default);

    ValueTask<TEntity?> GetByIdAsync(TPrimaryKeyType id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByReferenceAsync(Guid reference, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity?> SearchOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
