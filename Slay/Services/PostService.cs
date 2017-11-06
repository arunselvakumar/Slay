using System.Threading.Tasks;
using Slay.Models.BOs.Post;
using Slay.Services.Interfaces;
using Slay.Validators.Post;

namespace Slay.Services
{
    public sealed class PostService : IPostService
    {
        public Task<CreatePostResponseBo> CreatePostAsync(CreatePostRequestBo createPostRequestBo)
        {
            var validationResult = new CreatePostValidator().ValidateAsync(createPostRequestBo);

            return null;
        }
    }
}
