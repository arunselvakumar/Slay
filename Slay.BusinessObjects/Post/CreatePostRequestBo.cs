using System.Collections.Generic;
using Slay.BusinessObjects.Enums;

namespace Slay.BusinessObjects.Post
{
    public sealed class CreatePostRequestBo
    {
        public string Title { get; set; }

        public PostTypeEnum Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}