using Slay.Models.DataTransferObjects.Post.Links;

namespace Slay.Models.DataTransferObjects.Post.Response
{
	public sealed class PostResponseDto
	{
		public LinksDto Links { get; set; }

		public PostDto Data { get; set; }
	}
}