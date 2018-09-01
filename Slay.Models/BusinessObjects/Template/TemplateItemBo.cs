namespace Slay.Models.BusinessObjects.Template
{
    using System;

    public sealed class TemplateItemBo
    {
        public string Id { get; set; }

        public string PrimaryUrl { get; set; }

        public string SecondaryUrl { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}