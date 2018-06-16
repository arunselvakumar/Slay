namespace Slay.Models.DataTransferObjects.Category
{
    using System.Collections.Generic;

    public sealed class CategoriesListResponseDto
    {
        public IEnumerable<CategoryItemDto> Data { get; set; }
    }
}