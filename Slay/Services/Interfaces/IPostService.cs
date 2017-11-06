using Slay.Models.BOs.Post;
using System.Threading.Tasks;

namespace Slay.Services.Interfaces
{
    public interface IPostService
    {
        Task<CreatePostResponseBo> CreatePostAsync(CreatePostRequestBo createPostRequestBo);
    }
}
