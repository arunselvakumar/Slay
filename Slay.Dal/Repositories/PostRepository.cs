namespace Slay.Dal.Repositories
{
    using Slay.DalContracts.Repositories;
    using Slay.Models.Entities;

    public sealed class PostRepository : RepositoryBase<PostEntity>, IPostRepository
    {
        public PostRepository()
            : base("Slay", "Posts")
        {
        }
    }
}