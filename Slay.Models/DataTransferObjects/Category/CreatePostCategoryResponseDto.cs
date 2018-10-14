namespace Slay.Models.DataTransferObjects.Category
{
    using Slay.Models.DataTransferObjects.Shared;

    public sealed class CreatePostCategoryResponseDto
    {
        public LinksDto Links { get; set; }

        public PostCategoryItemDto Data { get; set; }

        public string TimeStamp { get; set; }
    }
}