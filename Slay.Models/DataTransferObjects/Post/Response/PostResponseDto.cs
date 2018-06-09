namespace Slay.Models.DataTransferObjects.Post.Response
{
    using System;

    using Slay.Models.DataTransferObjects.Post.Links;

    public sealed class PostResponseDto
    {
        public LinksDto Links { get; set; }

        public PostDto Data { get; set; }

        public string TimeStamp { get; set; }
    }
}