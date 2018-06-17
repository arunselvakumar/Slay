namespace Slay.Models.Entities
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    using Newtonsoft.Json;

    using Slay.Models.Entities.Interfaces;

    public sealed class CommentEntity : IEntity
    {
        [BsonId]
        [JsonProperty(PropertyName = "id")]
        public ObjectId Id { get; set; }

        [JsonProperty(PropertyName = "parentid")]
        public string ParentId { get; set; }

        [JsonProperty(PropertyName = "postid")]
        public string PostId { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "commentedby")]
        public string CommentedBy { get; set; }

        [JsonProperty(PropertyName = "createdon")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "modifiedon")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(PropertyName = "isdeleted")]
        public bool IsDeleted { get; set; }

        public void Delete()
        {
            this.IsDeleted = true;
        }
    }
}