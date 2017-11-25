using System.Collections.Generic;

namespace Slay.Models.BusinessObjects.Post
{
	public sealed class PostResponseBo
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Type { get; set; }

		public string Content { get; set; }

		public string Category { get; set; }

		public IEnumerable<string> Tags { get; set; }
	}
}
