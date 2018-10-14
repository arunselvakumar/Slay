namespace Slay.Models.DataTransferObjects.Post.Request
{
    using System;
    using System.Collections.Generic;

    public sealed class CreatePostRequestDto
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string IsAnonymous { get; set; }

        public int ExpiresIn { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<string> SearchTags { get; set; }
    }
}