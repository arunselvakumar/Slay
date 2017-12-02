using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Slay.Models.Entities.Interfaces;

namespace Slay.Models.Entities
{
	public sealed class CommentEntity : IEntity
	{
		[BsonId]
		[JsonProperty(PropertyName = "id")]
		public ObjectId Id { get; set; }

		[JsonProperty(PropertyName = "comment")]
		public string Comment { get; set; }

		[JsonProperty(PropertyName = "comments")]
		public IEnumerable<CommentEntity> Comments { get; set; }

		[JsonProperty(PropertyName = "isdeleted")]
		public bool IsDeleted { get; set; }

		[JsonProperty(PropertyName = "commentedby")]
		public string CommentedBy { get; set; }

		[JsonProperty(PropertyName = "createdon")]
		public DateTime CreatedOn { get; set; }

		public void Delete()
		{
			this.IsDeleted = true;
		}
	}
}