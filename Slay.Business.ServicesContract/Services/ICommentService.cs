namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.Comment;
    using Slay.Utilities.ServiceResult;

    public interface ICommentService
    {
        Task<ServiceResult<CommentItemBo>> CreateCommentAsync([NotNull] string postId, string commentId, [NotNull] CreateCommentRequestBo createCommentRequestBo, CancellationToken token);

        Task<ServiceResult<CommentsListResponseBo>> GetCommentsAsync([NotNull] string postId, string commentId, int skip, int limit, CancellationToken token);
    }
}