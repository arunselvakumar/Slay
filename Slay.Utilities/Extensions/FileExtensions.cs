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

        private static readonly IDictionary<string, string> AudioMimeDictionary = new Dictionary<string, string>
        {
            { ".aac", "audio/aac" },
            { ".oga", "audio/ogg" },
            { ".wav", "audio/wav" },
            { ".weba", "audio/webm" },
            { ".mp3", "audio/mpeg3" }
        };

        private static readonly IDictionary<string, string> VideoMimeDictionary = new Dictionary<string, string>
        {
            { ".avi", "video/x-msvideo" },
            { ".mpeg", "video/mpeg" },
            { ".ogv", "video/ogg" },
            { ".webm", "video/webm" }
        };

        /// <summary>
        /// Determines whether this instance is image.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <returns>
        ///   <c>true</c> if the specified this is image; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">File Name</exception>
        public static bool IsImageFile(this string @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var extension = Path.GetExtension(@this);
            return extension.IsNotNullOrEmpty() && ImageMimeDictionary.ContainsKey(extension.ToLowerInvariant());
        }

        /// <summary>
        /// Determines whether this instance is audio.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <returns>
        ///   <c>true</c> if the specified this is audio; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">File Name</exception>
        public static bool IsAudioFile(this string @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var extension = Path.GetExtension(@this);
            return extension.IsNotNullOrEmpty() && AudioMimeDictionary.ContainsKey(extension.ToLowerInvariant());
        }

        /// <summary>
        /// Determines whether this instance is video.
        /// </summary>
        /// <param name="this">The this.</param>
        /// <returns>
        ///   <c>true</c> if the specified this is video; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">File Name</exception>
        public static bool IsVideoFile(this string @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var extension = Path.GetExtension(@this);
            return extension.IsNotNullOrEmpty() && VideoMimeDictionary.ContainsKey(extension.ToLowerInvariant());
        }
    }
}