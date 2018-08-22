namespace Slay.Host.Controllers.ClientControllers
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/post/template")]
    [ApiController]
    public sealed class PostTemplateController : ApiBaseController
    {
        [HttpGet(Name = nameof(GetPostTemplatesAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostTemplatesAsync(CancellationToken token = default(CancellationToken))
        {
            return null;
        }

        [HttpGet(Name = nameof(CreatePostTemplateAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePostTemplateAsync(IFormCollection formCollection)
        {
            return null;
        }
    }
}