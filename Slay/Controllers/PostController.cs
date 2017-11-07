using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.Models.BOs.Post;
using Slay.Models.DTOs.Post;
using Slay.Services.Interfaces;

namespace Slay.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        private readonly IMapper mapper;

        public PostController(IMapper mapper, IPostService postService)
        {
            this.mapper = mapper;

            this.postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByIdAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var result = await this.postService.GetPostByIdAsync(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody]CreatePostRequestDto createPostDto)
        {
            if (createPostDto == null)
            {
                return BadRequest();
            }

            var createPostBo = this.mapper.Map<CreatePostRequestBo>(createPostDto);

            var result = await this.postService.CreatePostAsync(createPostBo);

            return CreatedAtRoute(string.Empty, null);
        }
    }
}