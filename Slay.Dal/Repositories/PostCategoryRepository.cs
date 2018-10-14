namespace Slay.Dal.Repositories
{
    using Slay.DalContracts.Repositories;
    using Slay.Models.Entities;

    public sealed class PostCategoryRepository : RepositoryBase<PostCategoryEntity>, IPostCategoryRepository
    {
        public PostCategoryRepository()
            : base("Slay", "Category")
        {
        }
    }
}