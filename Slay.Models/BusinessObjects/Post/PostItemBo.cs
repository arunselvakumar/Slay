﻿namespace Slay.Models.BusinessObjects.Post
{
    using System;
    using System.Collections.Generic;

    using Slay.Models.BusinessObjects.Comment;

    public sealed class PostItemBo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<string> SearchTags { get; set; }

        public CommentsListResponseBo Comments { get; set; }

        public bool IsAnonymous { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}