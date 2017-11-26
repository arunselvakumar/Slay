using JetBrains.Annotations;
using Slay.Models.Entities;
using System.Threading.Tasks;

namespace Slay.DalContracts.Repositories
{
	public interface IPostRepository
	{
		Task<PostEntity> CreatePostAsync([NotNull] PostEntity post);

		Task<PostEntity> GetPostByIdAsync([NotNull] string postId);

		Task<PostEntity> UpdatePostByIdAsync([NotNull] string postId, [NotNull] PostEntity post);

		Task<CommentEntity> CreateCommentAsync([NotNull] string postId, string commentId, [NotNull] CommentEntity commentEntity);
	}
}