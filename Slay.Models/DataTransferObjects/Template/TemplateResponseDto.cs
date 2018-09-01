namespace Slay.Models.DataTransferObjects.Template
{
    using Slay.Models.DataTransferObjects.Post.Response;
    using Slay.Models.DataTransferObjects.Shared;

    public sealed class TemplateResponseDto
    {
        public LinksDto Links { get; set; }

        public TemplateDto Data { get; set; }

        public string TimeStamp { get; set; }
    }
}