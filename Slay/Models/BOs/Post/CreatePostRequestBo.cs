using System.Collections.Generic;
using AutoMapper.Attributes;
using Slay.Models.DTOs.Post;

namespace Slay.Models.BOs.Post
{
    [MapsFrom(typeof(CreatePostRequestDto))]
    public sealed class CreatePostRequestBo
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
