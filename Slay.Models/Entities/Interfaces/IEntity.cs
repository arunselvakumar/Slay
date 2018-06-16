namespace Slay.Models.Entities.Interfaces
{
    using MongoDB.Bson;

    public interface IEntity
    {
        ObjectId Id { get; set; }

        bool IsDeleted { get; set; }

        void Delete();
    }
}