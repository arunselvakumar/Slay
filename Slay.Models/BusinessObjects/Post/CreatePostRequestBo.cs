namespace Slay.Models.BusinessObjects.Post
{
    using System.Collections.Generic;

    using Slay.Models.Enums;

    public sealed class CreatePostRequestBo
    {
        public string Title { get; set; }

        public PostTypeEnum Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public bool IsAnonymous { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<string> SearchTags { get; set; }
    }
}