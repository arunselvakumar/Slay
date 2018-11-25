namespace Slay.Models.DataTransferObjects.Post.Response
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Slay.Models.DataTransferObjects.Shared;

    public sealed class PostsListResponseDto
    {
        public LinksDto Links { get; set; }

        public IEnumerable<PostResponseDto> Data { get; set; }
    }
}