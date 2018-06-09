namespace Slay.Models.BusinessObjects.Comment
{
    using System.Collections.Generic;

    public sealed class CommentsListResponseBo
    {
        public IEnumerable<CommentItemBo> Comments { get; set; }

        public int? Skip { get; set; }

        public int? Limit { get; set; }
    }
}