using Newtonsoft.Json;
using Slay.Models.DataTransferObjects.Link;

namespace Slay.Models.DataTransferObjects.Comment
{
	public sealed class CommentResponseDto
	{
		[JsonProperty(PropertyName = "_links")]
		public LinksDto Links { get; set; }

		[JsonProperty(PropertyName = "_data")]
		public CommentItemDto Data { get; set; }
	}
}