using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.Models.BusinessObjects.Post;
using Slay.Models.DataTransferObjects.Post;
using Slay.ServicesContracts.Services;
using System.Threading.Tasks;

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

		[HttpGet("{id}", Name = Routes.GetPost)]
		public async Task<IActionResult> GetPostByIdAsync(string id)
		{
			try
			{
				var serviceResult = await this._postService.GetPostByIdAsync(id);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				if (serviceResult.Value == null)
				{
					return new NotFoundResult();
				}

				var mapperResult = this._mapper.Map<PostItemDto>(serviceResult.Value);

				return new OkObjectResult(mapperResult);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);

				return new NotFoundResult();
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetPostsAsync([FromQuery]int skip = 0, [FromQuery]int limit = 10)
		{
			try
			{
				var serviceResult = await this._postService.GetPostsAsync(skip, limit);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				var mapperResult = this._mapper.Map<PostsResponseDto>(serviceResult.Value);

				return new OkObjectResult(mapperResult);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return new EmptyResult();
			}
		}

		[HttpPost(Name = Routes.CreatePost)]
		public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequestDto createPostDto)
		{
			try
			{
				var createPostBo = this._mapper.Map<CreatePostRequestBo>(createPostDto);

				var serviceResult = await this._postService.CreatePostAsync(createPostBo);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				var mappedResult = this._mapper.Map<PostItemDto>(serviceResult.Value);

				return CreatedAtRoute(Routes.GetPost, new { id = mappedResult.Id }, mappedResult);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				
				return new BadRequestResult();
			}
		}

		[HttpDelete("{id}", Name = Routes.DeletePost)]
		public async Task<IActionResult> DeletePostAsync(string id)
		{
			try
			{
				var serviceResult = await this._postService.DeletePostAsync(id);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				return new EmptyResult();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				
				return new BadRequestResult();
			}
		}
	}
}