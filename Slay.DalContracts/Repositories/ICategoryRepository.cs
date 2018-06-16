namespace Slay.DalContracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.DalContracts.Options;
    using Slay.Models.Entities;

    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAsync(
            Expression<Func<CategoryEntity, bool>> filter,
            PagingOptions pagingOptions,
            IList<SortingOptions> sortingOptions,
            CancellationToken token);

        Task<CategoryEntity> CreateAsync([NotNull] CategoryEntity category, CancellationToken token);
    }
}