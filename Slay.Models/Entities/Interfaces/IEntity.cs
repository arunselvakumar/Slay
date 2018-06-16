namespace Slay.Models.Entities.Interfaces
{
    using System;

    using MongoDB.Bson;

    public interface IEntity
    {
        ObjectId Id { get; set; }

        DateTime CreatedOn { get; set; }

        DateTime ModifiedOn { get; set; }

        bool IsDeleted { get; set; }

        void Delete();
    }
}