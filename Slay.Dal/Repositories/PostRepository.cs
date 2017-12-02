using Slay.DalContracts.Repositories;
using Slay.Models.Entities;

namespace Slay.Dal.Repositories
{
	public sealed class PostRepository : RepositoryBase<PostEntity>, IPostRepository
	{
		public PostRepository() : base("Slay", "Posts")
		{
		}
	}
}