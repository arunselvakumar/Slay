using System.Collections.Generic;

namespace Slay.Models.DataTransferObjects.Post
{
	public sealed class CreatePostRequestDto
	{
		public string Title { get; set; }

		public string Type { get; set; }

		public string Content { get; set; }

		public string Category { get; set; }

		public string IsAnonymous { get; set; }

		public IEnumerable<string> Tags { get; set; }

		public IEnumerable<string> SearchTags { get; set; }
	}
}