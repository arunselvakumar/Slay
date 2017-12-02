using MongoDB.Bson;

namespace Slay.Models.Entities.Interfaces
{
	public interface IEntity
	{
		ObjectId Id { get; set; }

		bool IsDeleted { get; set; }

		void Delete();
	}
}