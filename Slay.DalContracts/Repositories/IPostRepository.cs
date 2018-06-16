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

    public interface IPostRepository
    {
        Task<PostEntity> GetByIdAsync([NotNull] string postId, CancellationToken token = default(CancellationToken));

        Task<IEnumerable<PostEntity>> GetAsync(
            Expression<Func<PostEntity, bool>> filter,
            [NotNull] PagingOptions pagingOptions,
            [NotNull] IList<SortingOptions> sortingOptions,
            CancellationToken token);

        Task<PostEntity> CreateAsync([NotNull] PostEntity post, CancellationToken token = default(CancellationToken));

        Task<PostEntity> UpdateAsync([NotNull] string postId, [NotNull] PostEntity post, CancellationToken token);

        Task<bool> DeleteAsync(string id, CancellationToken token = default(CancellationToken));

        Task<long> CountAsync(Expression<Func<PostEntity, bool>> filter, CancellationToken token);
    }
}