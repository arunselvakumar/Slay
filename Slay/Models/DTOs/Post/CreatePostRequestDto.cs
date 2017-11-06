using System.Collections.Generic;
using AutoMapper.Attributes;
using Slay.Models.BOs.Post;

namespace Slay.Models.DTOs.Post
{
    [MapsTo(typeof(CreatePostRequestBo))]
    public sealed class CreatePostRequestDto
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
