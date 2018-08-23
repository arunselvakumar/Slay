﻿namespace Slay.Models.BusinessObjects.File
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Http;

    public sealed class FileUploadRequestContext
    {
        public IFormFile File { get; set; }

        public ClaimsPrincipal User { get; set; }

        public string RequestType { get; set; }
    }
}