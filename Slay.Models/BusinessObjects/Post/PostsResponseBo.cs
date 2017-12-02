using System.Collections.Generic;

namespace Slay.Models.BusinessObjects.Post
{
	public sealed class PostsResponseBo
	{
		public IEnumerable<PostItemBo> Posts { get; set; }

		public int? Skip { get; set; }

		public int? Limit { get; set; }
	}
}