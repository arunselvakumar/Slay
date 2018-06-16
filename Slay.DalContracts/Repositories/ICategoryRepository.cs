namespace Slay.DalContracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.Entities;
    using Slay.Models.Enums;

    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync(HierarchicalLevelEnum hierarchicalLevel, CancellationToken token);

        Task<CategoryEntity> CreateCategoryAsync([NotNull]CategoryEntity category, CancellationToken token);
    }
}