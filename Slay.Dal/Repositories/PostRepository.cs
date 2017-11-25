using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Slay.Models.Entities;

namespace Slay.Dal.Repositories
{
	public sealed class PostRepository : RepositoryBase<PostEntity>
	{
		public PostRepository() : base("Slay", "Posts")
		{
		}

		public async Task<PostEntity> CreatePostAsync(PostEntity post)
		{
			try
			{
				await this.Collection.InsertOneAsync(post);
			}

			catch (Exception exception)
			{
				Console.WriteLine(exception);
				throw;
			}

			return await this.GetPostsByIdAsync(post.Id.ToString());
		}

		private async Task<PostEntity> GetPostsByIdAsync(string postId)
		{
			try
			{
				var filteredPostsCollection = await this.Collection.FindAsync(Builders<PostEntity>.Filter.Eq("_id", ObjectId.Parse(postId)));

				return await filteredPostsCollection.FirstOrDefaultAsync();
			}

			catch (Exception exception)
			{
				Console.WriteLine(exception);
				throw;
			}
		}
	}
}