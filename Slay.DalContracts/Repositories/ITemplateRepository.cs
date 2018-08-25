namespace Slay.DalContracts.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.Entities;

    public interface ITemplateRepository
    {
        Task<TemplateEntity> GetByIdAsync([NotNull] string templateId, CancellationToken token = default(CancellationToken));

        Task<TemplateEntity> CreateAsync([NotNull] TemplateEntity template, CancellationToken token = default(CancellationToken));

        Task<long> CountAsync(Expression<Func<TemplateEntity, bool>> filter, CancellationToken token);
    }
}