using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Slay.Models.Enums;
using System.Collections.Generic;

namespace Slay.Models.Entities
{
	public sealed class PostEntity
	{
		[BsonId]
		[JsonProperty(PropertyName = "id")]
		public ObjectId Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "type")]
		public PostTypeEnum Type { get; set; }

		[JsonProperty(PropertyName = "content")]
		public string Content { get; set; }

		[JsonProperty(PropertyName = "category")]
		public string Category { get; set; }

		[JsonProperty(PropertyName = "tags")]
		public IEnumerable<string> Tags { get; set; }

		[JsonProperty(PropertyName = "comments")]
		public IEnumerable<CommentEntity> Comments { get; set; }
	}
}