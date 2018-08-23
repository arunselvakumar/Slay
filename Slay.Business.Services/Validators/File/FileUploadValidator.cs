namespace Slay.Business.Services.Validators.File
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Validators;

    using Microsoft.AspNetCore.Http;

    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.Extensions;

    public sealed class FileUploadValidator : AbstractValidator<FileUploadRequestContext>
    {
        private readonly string _fileTypeShouldNotBeEmptyError = "FILETYPE_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _fileTypeShouldBeValidError = "FILETYPE_SHOULDBEVALID_ERROR";

        private readonly string _fileTypeNotSupportedError = "FILETYPE_NOTSUPPORTED_ERROR";

        public FileUploadValidator()
        {
            this.RuleFor(request => request.RequestType).NotEmpty().WithMessage(this._fileTypeShouldNotBeEmptyError);

            this.RuleFor(request => request.RequestType).Must(this.IsValidFileType)
                .WithMessage(this._fileTypeShouldBeValidError);

            this.RuleFor(request => request.File).Must(this.IsSupportedFileExtension)
                .WithMessage(this._fileTypeNotSupportedError);
        }

        private bool IsValidFileType(string fileType)
        {
            var vaildFileTypes = new[] { "image", "video", "audio" };
            return vaildFileTypes.Any(file => file == fileType);
        }

        private bool IsSupportedFileExtension(FileUploadRequestContext uploadRequestContext, IFormFile formFile, PropertyValidatorContext validatorContext)
        {
            if (uploadRequestContext.RequestType == "image")
            {
                return uploadRequestContext.File.FileName.IsImage();
            }

            return true;
        }
    }
}