using System.Collections.Generic;
using JetBrains.Annotations;
using Slay.Models.Entities;
using System.Threading.Tasks;
using Slay.DalContracts.Options;

namespace Slay.DalContracts.Repositories
{
	public interface IPostRepository
	{
		Task<PostEntity> GetPostByIdAsync([NotNull] string postId);

		Task<IEnumerable<PostEntity>> GetPosts([NotNull]PagingOptions pagingOptions, [NotNull]IList<SortingOptions> sortingOptions);

		Task<PostEntity> CreatePostAsync([NotNull] PostEntity post);

		Task<PostEntity> UpdatePostByIdAsync([NotNull] string postId, [NotNull] PostEntity post);

		Task<CommentEntity> CreateCommentAsync([NotNull] string postId, string commentId, [NotNull] CommentEntity commentEntity);
	}
}