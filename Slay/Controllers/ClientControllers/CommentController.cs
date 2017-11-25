using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.Models.BusinessObjects.Comment;
using Slay.Models.DataTransferObjects.Comment;
using Slay.ServicesContracts.Services;

namespace Slay.Host.Controllers.ClientControllers
{
	[Produces("application/json")]
    [Route("api/Post/{postId}/Comment")]
    public class CommentController : ControllerBase
    {
	    private readonly IMapper _mapper;

	    private readonly ICommentService _commentService;

	    public CommentController(IMapper mapper, ICommentService commentService)
	    {
		    this._mapper = mapper;

		    this._commentService = commentService;
	    }

	    [HttpPost]
	    public async Task<IActionResult> CreateCommentAsync(string postId, [FromBody]CreateCommentRequestDto createCommentRequestDto)
	    {
		    var createCommentBo = this._mapper.Map<CreateCommentRequestBo>(createCommentRequestDto);

		    var serviceResult = await this._commentService.CreateCommentAsync(postId, string.Empty, createCommentBo);

		    if (serviceResult.HasErrors)
		    {
			    return new BadRequestObjectResult(serviceResult.Errors);
		    }

			return new OkObjectResult(this._mapper.Map<CommentResponseDto>(serviceResult.Value));
		}
    }
}