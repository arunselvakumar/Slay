namespace Slay.Business.ServicesContracts.Aggregators
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.Post;

    public interface ICommentAggregationService
    {
        Task AggregateAsync([NotNull]PostItemBo post);
    }
}