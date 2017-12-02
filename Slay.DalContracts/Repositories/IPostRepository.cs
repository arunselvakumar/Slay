using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using JetBrains.Annotations;
using Slay.Models.Entities;
using System.Threading.Tasks;
using Slay.DalContracts.Options;

namespace Slay.DalContracts.Repositories
{
	public interface IPostRepository
	{
		Task<PostEntity> GetByIdAsync([NotNull] string postId, CancellationToken token = default(CancellationToken));

		Task<IEnumerable<PostEntity>> GetAsync(Expression<Func<PostEntity, bool>> filter, [NotNull]PagingOptions pagingOptions, [NotNull]IList<SortingOptions> sortingOptions, CancellationToken token = default(CancellationToken));

		Task<PostEntity> CreateAsync([NotNull] PostEntity post, CancellationToken token = default(CancellationToken));

		Task<PostEntity> UpdateAsync([NotNull] string postId, [NotNull] PostEntity post, CancellationToken token = default(CancellationToken));

		Task<bool> DeleteAsync(string id, CancellationToken token = default(CancellationToken));

		Task<long> CountAsync(Expression<Func<PostEntity, bool>> filter, CancellationToken token = default(CancellationToken));
	}
}