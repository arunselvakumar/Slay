namespace Slay.Business.Services.Aggregators
{
    using System.Threading;
    using System.Threading.Tasks;

    using Slay.Business.ServicesContracts.Aggregators;
    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.Post;
    using Slay.Utilities.Extensions;

    public sealed class CommentAggregationService : ICommentAggregationService
    {
        private readonly ICommentService _commentService;

        public CommentAggregationService(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        public async Task AggregateAsync(PostItemBo post, CancellationToken token)
        {
            if (post.IsNull())
            {
                return;
            }

            var commentServiceResult = await this._commentService.GetCommentsAsync(post.Id, null, 0, 10, token);
            post.Comments = commentServiceResult.Value;
        }
    }
}