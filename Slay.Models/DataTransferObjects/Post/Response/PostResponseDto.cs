namespace Slay.Models.DataTransferObjects.Post.Response
{
    using Slay.Models.DataTransferObjects.Shared;

    public sealed class PostResponseDto
    {
        public LinksDto Links { get; set; }

        public PostDto Data { get; set; }

        public string TimeStamp { get; set; }
    }
}