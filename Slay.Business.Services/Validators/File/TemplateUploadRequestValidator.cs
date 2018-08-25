namespace Slay.Business.Services.Validators.File
{
    using FluentValidation;

    using Microsoft.AspNetCore.Http;

    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.Extensions;

    public sealed class TemplateUploadRequestValidator : AbstractValidator<TemplateUploadRequestContext>
    {
        private readonly string _fileTypeNotSupportedError = "TEMPLATE_FILETYPE_NOTSUPPORTED_ERROR";

        private readonly string _fileSizeShouldNotExceedSizeError = "TEMPLATE_FILESIZE_EXCEEDED_ERROR";

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateUploadRequestValidator"/> class.
        /// </summary>
        public TemplateUploadRequestValidator()
        {
            this.RuleFor(request => request.File).Must(this.IsSupportedFileExtension)
                .WithMessage(this._fileTypeNotSupportedError);

            this.RuleFor(request => request.File).Must(this.IsFileSizeExceeded)
                .WithMessage(this._fileSizeShouldNotExceedSizeError);
        }

        private bool IsSupportedFileExtension(IFormFile file)
        {
            var fileName = file.FileName;
            return fileName.IsImageFileType();
        }

        private bool IsFileSizeExceeded(IFormFile file)
        {
            var fileSize = file.Length;

            // 5 MB = 5e+6 Bytes;
            return fileSize < 5e+6;
        }
    }
}