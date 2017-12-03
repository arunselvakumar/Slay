using System.Collections.Generic;
using Slay.Models.DataTransferObjects.Link;

namespace Slay.Models.DataTransferObjects.Comment
{
	public class CommentsResponseDto
	{
		public LinksDto Links { get; set; }

		public IEnumerable<CommentResponseDto> Data { get; set; }
	}
}