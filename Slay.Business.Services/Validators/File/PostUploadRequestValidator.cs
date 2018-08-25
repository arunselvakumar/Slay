namespace Slay.Business.Services.Validators.File
{
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Validators;

    using Microsoft.AspNetCore.Http;

    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.Extensions;

    public sealed class PostUploadRequestValidator : AbstractValidator<PostUploadRequestContext>
    {
        private readonly string _fileTypeShouldNotBeEmptyError = "POST_FILETYPE_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _fileTypeShouldBeValidError = "POST_FILETYPE_SHOULDBEVALID_ERROR";

        private readonly string _fileTypeNotSupportedError = "POST_FILETYPE_NOTSUPPORTED_ERROR";

        private readonly string _fileSizeShouldNotExceedSizeError = "POST_FILESIZE_EXCEEDED_ERROR";

        /// <summary>
        /// Initializes a new instance of the <see cref="PostUploadRequestValidator"/> class.
        /// </summary>
        public PostUploadRequestValidator()
        {
            this.RuleFor(request => request.RequestType).NotEmpty().WithMessage(this._fileTypeShouldNotBeEmptyError);

            this.RuleFor(request => request.RequestType).Must(this.IsValidFileType)
                .WithMessage(this._fileTypeShouldBeValidError);

            this.RuleFor(request => request.File).Must(this.IsSupportedFileExtension)
                .WithMessage(this._fileTypeNotSupportedError);

            this.RuleFor(request => request.File).Must(this.IsFileSizeExceeded)
                .WithMessage(this._fileSizeShouldNotExceedSizeError);
        }

        private bool IsValidFileType(string fileType)
        {
            var vaildFileTypes = new[] { "image", "video", "audio" };
            return vaildFileTypes.Any(file => file == fileType);
        }

        private bool IsSupportedFileExtension(PostUploadRequestContext uploadRequestContext, IFormFile formFile, PropertyValidatorContext validatorContext)
        {
            var fileName = uploadRequestContext.File.FileName;

            switch (uploadRequestContext.RequestType.ToLowerInvariant())
            {
                case "image":
                    return fileName.IsImageFileType();
                case "audio":
                    return fileName.IsAudioFileType();
                case "video":
                    return fileName.IsVideoFileType();
                default:
                    return false;
            }
        }

        private bool IsFileSizeExceeded(PostUploadRequestContext uploadRequestContext, IFormFile formFile, PropertyValidatorContext validatorContext)
        {
            var fileSize = uploadRequestContext.File.Length;

            switch (uploadRequestContext.RequestType.ToLowerInvariant())
            {
                case "image":
                    // 5 MB = 5e+6 Bytes
                    return fileSize < 5e+6;
                case "audio":
                    // 30 MB = 3e+7 Bytes
                    return fileSize < 3e+7;
                case "video":
                    // 100 MB = 1e+8 Bytes.
                    return fileSize < 1e+8;
                default:
                    return false;
            }
        }
    }
}