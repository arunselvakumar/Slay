using System.Collections.Generic;
using Newtonsoft.Json;
using Slay.Models.DataTransferObjects.Link;

namespace Slay.Models.DataTransferObjects.Post
{
	public sealed class PostsResponseDto
	{
		[JsonProperty(PropertyName = "_links")]
		public LinksDto Links { get; set; }

		[JsonProperty(PropertyName = "_data")]
		public IEnumerable<PostResponseDto> Data { get; set; }
	}
}