namespace Slay.Models.Entities
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    using Newtonsoft.Json;

    using Slay.Models.Entities.Interfaces;

    public sealed class UserIdentityEntity : IEntity
    {
        [BsonId]
        [JsonProperty(PropertyName = "id")]
        public ObjectId Id { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phonenumber")]
        public string PhoneNumber { get; set; }

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