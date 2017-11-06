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
    public class PostController : Controller
    {
        private readonly IPostService postService;

        private readonly IMapper mapper;

        public PostController(IMapper mapper, IPostService postService)
        {
            this.mapper = mapper;

            this.postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody]CreatePostRequestDto createPostDto)
        {
            var createPostBo = this.mapper.Map<CreatePostRequestBo>(createPostDto);

            var result = await this.postService.CreatePostAsync(createPostBo);

            return CreatedAtRoute(string.Empty, null);
        }
    }
}