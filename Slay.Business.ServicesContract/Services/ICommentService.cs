#pragma warning disable 1998
namespace Slay.Business.ServicesContracts.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.Comment;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    [ContractClass(typeof(ICommentServiceContract))]
    public interface ICommentService
    {
        Task<ServiceResult<CommentItemBo>> CreateCommentAsync([NotNull] string postId, string parentCommentId, [NotNull] CreateCommentRequestBo createCommentRequestBo, CancellationToken token);

        Task<ServiceResult<CommentsListResponseBo>> GetCommentsAsync([NotNull] string postId, string parentCommentId, int skip, int limit, CancellationToken token);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts")]
    [ContractClassFor(typeof(ICommentService))]
    internal abstract class ICommentServiceContract : ICommentService
    {
        public async Task<ServiceResult<CommentItemBo>> CreateCommentAsync(
            string postId,
            string parentCommentId,
            CreateCommentRequestBo createCommentRequestBo,
            CancellationToken token)
        {
            Contract.Requires(postId.IsNotNullOrEmpty());
            Contract.Requires(createCommentRequestBo.IsNotNull());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<CommentItemBo>>().IsNotNull());

            return default(ServiceResult<CommentItemBo>);
        }

        public async Task<ServiceResult<CommentsListResponseBo>> GetCommentsAsync(string postId, string parentCommentId, int skip, int limit, CancellationToken token)
        {
            Contract.Requires(postId.IsNotNullOrEmpty());
            Contract.Requires(skip >= 0);
            Contract.Requires(limit >= 0);
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<CommentsListResponseBo>>().IsNotNull());

            return default(ServiceResult<CommentsListResponseBo>);
        }
    }
}