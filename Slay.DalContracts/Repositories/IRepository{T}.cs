namespace Slay.DalContracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Slay.DalContracts.Options;
    using Slay.Models.Entities.Interfaces;

    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<long> CountAsync(Expression<Func<T, bool>> filter, CancellationToken token);

        Task<T> CreateAsync(T entity, CancellationToken token);

        Task<bool> DeleteAsync(string id, CancellationToken token);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken token);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, PagingOptions pagingOptions, IList<SortingOptions> sortingOptions, CancellationToken token);

        Task<T> GetByIdAsync(string id, CancellationToken token);

        Task<T> UpdateAsync(string id, T entity, CancellationToken token);
    }
}