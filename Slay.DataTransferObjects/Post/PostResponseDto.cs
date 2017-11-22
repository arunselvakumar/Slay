using System.Collections.Generic;

namespace Slay.DataTransferObjects.Post
{
    public sealed class PostResponseDto
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}