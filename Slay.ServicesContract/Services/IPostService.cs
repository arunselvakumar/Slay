using JetBrains.Annotations;

using Slay.BusinessObjects.Post;
using Slay.Utilities.ServiceResult;

using System.Threading.Tasks;

namespace Slay.ServicesContracts.Services
{
	public interface IPostService
    {
        Task<ServiceResult<PostResponseBo>> GetPostByIdAsync([NotNull]string id);

        Task<ServiceResult<PostResponseBo>> CreatePostAsync([NotNull]CreatePostRequestBo createPostRequestBo);
    }
}