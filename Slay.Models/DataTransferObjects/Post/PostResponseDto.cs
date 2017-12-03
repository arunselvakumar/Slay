using Slay.Models.DataTransferObjects.Link;

namespace Slay.Models.DataTransferObjects.Post
{
	public sealed class PostResponseDto
	{
		public LinksDto Links { get; set; }

		public PostItemDto Data { get; set; }
	}
}