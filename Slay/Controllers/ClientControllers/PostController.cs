using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.Models.BusinessObjects.Post;
using Slay.Models.DataTransferObjects.Post;
using Slay.ServicesContracts.Services;
using System.Threading.Tasks;
using Slay.Models.DataTransferObjects.Link;

namespace Slay.Host.Controllers.ClientControllers
{
	[Produces("application/json")]
	[Route("api/Post")]
	public sealed class PostController : ControllerBase
	{
		private readonly IPostService _postService;

		private readonly IMapper _mapper;

		public PostController(IMapper mapper, IPostService postService)
		{
			this._mapper = mapper;

			this._postService = postService;
		}

		[HttpGet("{id}", Name = nameof(GetPostByIdAsync))]
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

				var mapperResult = this._mapper.Map<PostResponseDto>(serviceResult.Value);

				mapperResult.Links = new LinksDto
				{
					Base = this.GetBaseUrl(),
					Self = Url.Link(nameof(GetPostByIdAsync), new { id = mapperResult.Data.Id } )
				};

				return new OkObjectResult(mapperResult);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);

				return new NotFoundResult();
			}
		}

		[HttpGet(Name = nameof(GetPostsAsync))]
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

				mapperResult.Links = new LinksDto
				{
					Base = this.GetBaseUrl(),
					Self = Url.Link(nameof(GetPostsAsync), new { skip = (int?) skip, limit = (int?) limit}),
					Next = Url.Link(nameof(GetPostsAsync), new { skip = serviceResult.Value.Skip, limit = serviceResult.Value.Limit })
				};

				mapperResult.Data.ToList().ForEach(post => post.Links = new LinksDto {Base = this.GetBaseUrl(), Self = Url.Link(nameof(GetPostsAsync), new {id = post.Data.Id})});

				return new OkObjectResult(mapperResult);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return new EmptyResult();
			}
		}

		[HttpPost(Name = nameof(CreatePostAsync))]
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

				return CreatedAtRoute(nameof(CreatePostAsync), new { id = mappedResult.Id }, mappedResult);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				
				return new BadRequestResult();
			}
		}

		[HttpDelete("{id}", Name = nameof(DeletePostAsync))]
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

		private string GetBaseUrl()
		{
			return Request.Scheme + "://" + Request.Host + Request.PathBase.Value.TrimEnd('/') + "/";
		}
	}
}