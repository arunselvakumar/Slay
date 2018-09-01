namespace Slay.DalContracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.DalContracts.Options;
    using Slay.Models.Entities;

    public interface ITemplateRepository
    {
        Task<IEnumerable<TemplateEntity>> GetAsync(
            Expression<Func<TemplateEntity, bool>> filter,
            [NotNull] PagingOptions pagingOptions,
            [NotNull] IList<SortingOptions> sortingOptions,
            CancellationToken token);

        Task<TemplateEntity> GetByIdAsync(
            [NotNull] string templateId,
            CancellationToken token = default(CancellationToken));

        Task<TemplateEntity> CreateAsync(
            [NotNull] TemplateEntity template,
            CancellationToken token = default(CancellationToken));

        Task<long> CountAsync(Expression<Func<TemplateEntity, bool>> filter, CancellationToken token);
    }
}