namespace Slay.Models.DataTransferObjects.Comment
{
    using System.Collections.Generic;

    using Slay.Models.DataTransferObjects.Post.Links;

    public class CommentsListResponseDto
    {
        public LinksDto Links { get; set; }

        public IEnumerable<CommentResponseDto> Data { get; set; }
    }
}