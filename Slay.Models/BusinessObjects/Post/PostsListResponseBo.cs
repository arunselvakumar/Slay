namespace Slay.Models.BusinessObjects.Post
{
    using System.Collections.Generic;

    public sealed class PostsListResponseBo
    {
        public IEnumerable<PostItemBo> Posts { get; set; }

        public int? Skip { get; set; }

        public int? Limit { get; set; }
    }
}