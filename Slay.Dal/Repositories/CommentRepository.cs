using Slay.DalContracts.Repositories;
using Slay.Models.Entities;

namespace Slay.Dal.Repositories
{
	public sealed class CommentRepository : RepositoryBase<CommentEntity>, ICommentRepository
	{
		public CommentRepository() : base("Slay", "Comments")
		{
		}
	}
}