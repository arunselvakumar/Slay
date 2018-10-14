namespace Slay.Models.Entities
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    using Newtonsoft.Json;

    using Slay.Models.Entities.Interfaces;
    using Slay.Models.Enums;

    public sealed class PostEntity : IEntity
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

        [JsonProperty(PropertyName = "isanonymous")]
        public bool IsAnonymous { get; set; }

        [JsonProperty(PropertyName = "expiresin")]
        public TimeSpan ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "modifiedby")]
        public string ModifiedBy { get; set; }

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