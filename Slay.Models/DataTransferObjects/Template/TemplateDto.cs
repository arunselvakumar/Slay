namespace Slay.Models.DataTransferObjects.Template
{
    using System;

    public sealed class TemplateDto
    {
        public string Id { get; set; }

        public string PrimaryUrl { get; set; }

        public string SecondaryUrl { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}