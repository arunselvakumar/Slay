using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Slay.DalContracts.Options;
using Slay.Models.Entities;

namespace Slay.DalContracts.Repositories
{
	public interface ICommentRepository
	{
		Task<IEnumerable<CommentEntity>> GetAsync(Expression<Func<CommentEntity, bool>> filter, [NotNull]PagingOptions pagingOptions, [NotNull]IList<SortingOptions> sortingOptions, CancellationToken token = default(CancellationToken));

		Task<CommentEntity> CreateAsync([NotNull] CommentEntity post, CancellationToken token = default(CancellationToken));

		Task<long> CountAsync(Expression<Func<CommentEntity, bool>> filter, CancellationToken token = default(CancellationToken));
	}
}