using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.BusinessObjects.Post;
using Slay.DataTransferObjects.Post;
using Slay.ServicesContracts.Services;

namespace Slay.Host.Controllers.ClientControllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        private readonly IMapper _mapper;

        public PostController(IMapper mapper, IPostService postService)
        {
            this._mapper = mapper;

            this._postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByIdAsync(string id)
        {
            var result = await this._postService.GetPostByIdAsync(id);

            if (result.HasErrors)
            {
                return NotFound();
            }

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody]CreatePostRequestDto createPostDto)
        {
            var createPostBo = this._mapper.Map<CreatePostRequestBo>(createPostDto);

            var result = await this._postService.CreatePostAsync(createPostBo);

            if (result.HasErrors)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            return CreatedAtRoute(string.Empty, result);
        }
    }
}