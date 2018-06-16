namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.Post;
    using Slay.Utilities.ServiceResult;

    public interface IPostService
    {
        Task<ServiceResult<PostItemBo>> GetPostByIdAsync([NotNull] string id, CancellationToken token);

        Task<ServiceResult<PostItemBo>> CreatePostAsync([NotNull] CreatePostRequestBo createPostRequestBo, CancellationToken token);

        Task<ServiceResult<bool>> DeletePostAsync([NotNull]string id, CancellationToken token);

        Task<ServiceResult<PostsListResponseBo>> GetPostsAsync(int skip, int limit, CancellationToken token);
    }
}