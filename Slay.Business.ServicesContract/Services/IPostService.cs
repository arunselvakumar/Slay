#pragma warning disable 1998
namespace Slay.Business.ServicesContracts.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Post;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    [ContractClass(typeof(IPostServiceContract))]
    public interface IPostService
    {
        Task<ServiceResult<PostItemBo>> GetPostByIdAsync([NotNull] string id, CancellationToken token);

        Task<ServiceResult<PostItemBo>> CreatePostAsync([NotNull] CreatePostRequestBo createPostRequestBo, CancellationToken token);

        Task<ServiceResult<bool>> DeletePostAsync([NotNull] string id, CancellationToken token);

        Task<ServiceResult<PostsListResponseBo>> GetPostsAsync(int skip, int limit, CancellationToken token);

        Task<ServiceResult<PostUploadResponseContext>> UploadPostAsync([NotNull] PostUploadRequestContext uploadRequestContext, CancellationToken token);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts")]
    [ContractClassFor(typeof(IPostService))]
    internal abstract class IPostServiceContract : IPostService
    {
        public async Task<ServiceResult<PostItemBo>> GetPostByIdAsync(string id, CancellationToken token)
        {
            Contract.Requires(id.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<PostItemBo>>().IsNotNull());

            return default(ServiceResult<PostItemBo>);
        }

        public async Task<ServiceResult<PostItemBo>> CreatePostAsync(CreatePostRequestBo createPostRequestBo, CancellationToken token)
        {
            Contract.Requires(createPostRequestBo.IsNotNull());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<PostItemBo>>().IsNotNull());

            return default(ServiceResult<PostItemBo>);
        }

        public async Task<ServiceResult<bool>> DeletePostAsync(string id, CancellationToken token)
        {
            Contract.Requires(id.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<bool>>().IsNotNull());

            return default(ServiceResult<bool>);
        }

        public async Task<ServiceResult<PostsListResponseBo>> GetPostsAsync(int skip, int limit, CancellationToken token)
        {
            Contract.Requires(skip >= 0);
            Contract.Requires(limit >= 0);
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<bool>>().IsNotNull());

            return default(ServiceResult<PostsListResponseBo>);
        }

        public async Task<ServiceResult<PostUploadResponseContext>> UploadPostAsync(PostUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            Contract.Requires(uploadRequestContext.IsNotNull());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<PostUploadResponseContext>>().IsNotNull());

            return default(ServiceResult<PostUploadResponseContext>);
        }
    }
}