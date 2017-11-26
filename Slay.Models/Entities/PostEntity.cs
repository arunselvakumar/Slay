using System;
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

		[JsonProperty(PropertyName = "searchtags")]
		public IEnumerable<string> SearchTags { get; set; }

		[JsonProperty(PropertyName = "comments")]
		public IEnumerable<CommentEntity> Comments { get; set; }

		[JsonProperty(PropertyName = "isanonymous")]
		public bool IsAnonymous { get; set; }

		[JsonProperty(PropertyName = "createdby")]
		public string CreatedBy { get; set; }

		[JsonProperty(PropertyName = "modifiedby")]
		public string ModifiedBy { get; set; }

		[JsonProperty(PropertyName = "createdon")]
		public DateTime CreatedOn { get; set; }

		[JsonProperty(PropertyName = "modifiedon")]
		public DateTime ModifiedOn { get; set; }
	}
}