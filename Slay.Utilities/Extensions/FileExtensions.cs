namespace Slay.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class FileExtensions
    {
        private static readonly IDictionary<string, string> ImageMimeDictionary = new Dictionary<string, string>
        {
            { ".bmp", "image/bmp" },
            { ".dib", "image/bmp" },
            { ".gif", "image/gif" },
            { ".svg", "image/svg+xml" },
            { ".jpe", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" },
            { ".pnz", "image/png" }
        };

        public static bool IsImage(this string @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var extension = Path.GetExtension(@this);
            return extension.IsNotNullOrEmpty() && ImageMimeDictionary.ContainsKey(extension.ToLowerInvariant());
        }
    }
}