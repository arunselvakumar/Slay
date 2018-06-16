namespace Slay.Dal.Repositories
{
    using Slay.DalContracts.Repositories;
    using Slay.Models.Entities;

    public sealed class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository()
            : base("Slay", "Category")
        {
        }
    }
}