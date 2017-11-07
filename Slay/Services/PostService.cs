using System.Threading.Tasks;
using JetBrains.Annotations;
using Slay.Models.BOs.Post;
using Slay.Services.Interfaces;
using Slay.Validators.Post;

namespace Slay.Services
{
    public sealed class PostService : IPostService
    {
        public Task<PostResponseBo> GetPostByIdAsync(string id)
        {
            return null;
        }

        public Task<PostResponseBo> CreatePostAsync(CreatePostRequestBo createPostRequestBo)
        {
            var validationResult = new CreatePostValidator().ValidateAsync(createPostRequestBo);

            return null;
        }
    }
}
