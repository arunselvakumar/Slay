namespace Slay.Models.BusinessObjects.Template
{
    using System.Collections.Generic;

    public sealed class TemplateListResponseBo
    {
        public IEnumerable<TemplateItemBo> Templates { get; set; }

        public int? Skip { get; set; }

        public int? Limit { get; set; }
    }
}