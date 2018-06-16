namespace Slay.Dal.Repositories
{
    using Slay.DalContracts.Repositories;
    using Slay.Models.Entities;

    public sealed class CommentRepository : RepositoryBase<CommentEntity>, ICommentRepository
    {
        public CommentRepository()
            : base("Slay", "Comments")
        {
        }
    }
}