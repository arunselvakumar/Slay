namespace Slay.Models.BusinessObjects.Category
{
    using System.Collections.Generic;

    public sealed class CategoriesListResponseBo
    {
        public IEnumerable<CategoryItemBo> Categories { get; set; }
    }
}