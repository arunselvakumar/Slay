using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.Models.BOs.Post;
using Slay.Models.DTOs.Post;
using Slay.Services.Interfaces;

namespace Slay.Host.Controllers.Client
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
            if (id == null)
            {
                return BadRequest();
            }

            var result = await this._postService.GetPostByIdAsync(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody]CreatePostRequestDto createPostDto)
        {
            if (createPostDto == null)
            {
                return BadRequest();
            }

            var createPostBo = this._mapper.Map<CreatePostRequestBo>(createPostDto);

            var result = await this._postService.CreatePostAsync(createPostBo);

            return CreatedAtRoute(string.Empty, result);
        }
    }
}