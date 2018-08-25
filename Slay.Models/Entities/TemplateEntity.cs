namespace Slay.Models.Entities
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    using Newtonsoft.Json;

    using Slay.Models.Entities.Interfaces;

    public sealed class TemplateEntity : IEntity
    {
        [BsonId]
        [JsonProperty(PropertyName = "id")]
        public ObjectId Id { get; set; }

        [JsonProperty(PropertyName = "primayurl")]
        public string PrimaryUrl { get; set; }

        [JsonProperty(PropertyName = "secondaryurl")]
        public string SecondaryUrl { get; set; }

        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }

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