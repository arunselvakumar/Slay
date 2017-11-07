using Slay.Models.BOs.Post;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Slay.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostResponseBo> GetPostByIdAsync([NotNull]string id);

        Task<PostResponseBo> CreatePostAsync([NotNull]CreatePostRequestBo createPostRequestBo);
    }
}
