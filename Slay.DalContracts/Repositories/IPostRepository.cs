using JetBrains.Annotations;
using Slay.Models.Entities;
using System.Threading.Tasks;

namespace Slay.DalContracts.Repositories
{
	public interface IPostRepository
	{
		Task<PostEntity> CreatePostAsync([NotNull]PostEntity post);

		Task<PostEntity> GetPostsByIdAsync([NotNull]string postId);
	}
}