namespace Slay.Models.DataTransferObjects.Comment
{
    using Newtonsoft.Json;

    using Slay.Models.DataTransferObjects.Post.Links;

    public sealed class CommentResponseDto
    {
        [JsonProperty(PropertyName = "_links")]
        public LinksDto Links { get; set; }

        [JsonProperty(PropertyName = "_data")]
        public CommentItemDto Data { get; set; }
    }
}