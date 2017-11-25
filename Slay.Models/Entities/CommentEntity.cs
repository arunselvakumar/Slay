﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Slay.Models.Entities
{
	public sealed class CommentEntity
	{
		[BsonId]
		[JsonProperty(PropertyName = "id")]
		public ObjectId Id { get; set; }

		[JsonProperty(PropertyName = "comment")]
		public string Comment { get; set; }

		[JsonProperty(PropertyName = "comments")]
		public IEnumerable<CommentEntity> Comments { get; set; }

		[JsonProperty(PropertyName = "commentedby")]
		public string CommentedBy { get; set; }

		[JsonProperty(PropertyName = "createdon")]
		public DateTime CreatedOn { get; set; }
	}
}