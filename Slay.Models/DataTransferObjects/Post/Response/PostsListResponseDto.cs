namespace Slay.Models.DataTransferObjects.Post.Response
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Slay.Models.DataTransferObjects.Post.Links;

    public sealed class PostsListResponseDto
    {
        [JsonProperty(PropertyName = "_links")]
        public LinksDto Links { get; set; }

        [JsonProperty(PropertyName = "_data")]
        public IEnumerable<PostResponseDto> Data { get; set; }
    }
}