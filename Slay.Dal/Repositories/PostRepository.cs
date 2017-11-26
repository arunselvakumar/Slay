using MongoDB.Bson;
using MongoDB.Driver;
using Slay.DalContracts.Repositories;
using Slay.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slay.Dal.Repositories
{
	public sealed class PostRepository : RepositoryBase<PostEntity>, IPostRepository
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

			return await this.GetPostByIdAsync(post.Id.ToString());
		}

		public async Task<PostEntity> UpdatePostByIdAsync(string postId, PostEntity post)
		{
			try
			{
				await this.Collection.ReplaceOneAsync(Builders<PostEntity>.Filter.Eq("_id", ObjectId.Parse(postId)), post);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return await this.GetPostByIdAsync(postId);
		}

		public async Task<PostEntity> GetPostByIdAsync(string postId)
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

		public async Task<CommentEntity> CreateCommentAsync(string postId, string commentId, CommentEntity comment)
		{
			try
			{
				var filteredPostsCollection = await this.Collection.FindAsync(Builders<PostEntity>.Filter.Eq("_id", ObjectId.Parse(postId)));

				var postEntity = await filteredPostsCollection.FirstOrDefaultAsync();

				if (string.IsNullOrEmpty(commentId))
				{
					postEntity.Comments = postEntity.Comments.Append(comment);
				}
				else
				{
					foreach (var postComment in postEntity.Comments)
					{
						if (postComment.Id.ToString() == commentId)
						{
							postComment.Comments = postComment.Comments.Append(comment);
						}
					}
				}

				await this.UpdatePostByIdAsync(postId, postEntity);

				return comment;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}