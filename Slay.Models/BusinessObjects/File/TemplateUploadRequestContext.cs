namespace Slay.Models.BusinessObjects.File
{
    using Microsoft.AspNetCore.Http;

    public sealed class TemplateUploadRequestContext
    {
        public IFormFile File { get; set; }
    }
}