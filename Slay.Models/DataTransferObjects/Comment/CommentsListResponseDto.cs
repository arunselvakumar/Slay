namespace Slay.Models.DataTransferObjects.Comment
{
    using System.Collections.Generic;

    using Slay.Models.DataTransferObjects.Shared;

    public class CommentsListResponseDto
    {
        public LinksDto Links { get; set; }

        public IEnumerable<CommentResponseDto> Data { get; set; }
    }
}