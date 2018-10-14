namespace Slay.Models.DataTransferObjects.Category
{
    using System.Collections.Generic;

    public sealed class PostCategoriesListResponseDto
    {
        public IEnumerable<PostCategoryItemDto> Data { get; set; }
    }
}