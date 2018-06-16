namespace Slay.Models.DataTransferObjects.Category
{
    using Slay.Models.DataTransferObjects.Post.Links;

    public sealed class CreateCategoryResponseDto
    {
        public LinksDto Links { get; set; }

        public CategoryItemDto Data { get; set; }

        public string TimeStamp { get; set; }
    }
}