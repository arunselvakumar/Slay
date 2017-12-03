using System;
using System.Linq;
using AutoMapper;
using MongoDB.Bson;
using Slay.Models.BusinessObjects.Comment;
using Slay.Models.BusinessObjects.Post;
using Slay.Models.DataTransferObjects.Comment;
using Slay.Models.DataTransferObjects.Post;
using Slay.Models.Entities;
using Slay.Models.Enums;
using Slay.Utilities.Extensions;

namespace Slay.Host.Configuration
{
	public sealed class AutoMapperProfile : Profile
    {
	    public AutoMapperProfile()
	    {
		    this.ConfigurePostMappers();
	        this.ConfigureCommentMappers();
        }

	    private void ConfigurePostMappers()
	    {
		    this.CreateMap<CreatePostRequestDto, CreatePostRequestBo>()
				.ForMember(postBo => postBo.Type, opt => opt.MapFrom(x => x.Type.ToEnum<PostTypeEnum>()));
	        this.CreateMap<CreatePostRequestBo, PostEntity>()
				.ForMember(postEntity => postEntity.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
				.ForMember(postEntity => postEntity.ModifiedOn, opt => opt.MapFrom(x => DateTime.UtcNow));

	        this.CreateMap<PostEntity, PostItemBo>()
				.ForMember(postEntity => postEntity.CreatedBy, opt => opt.MapFrom(x => x.IsAnonymous ? string.Empty : x.CreatedBy));

			this.CreateMap<PostItemBo, PostItemDto>();
		    this.CreateMap<PostItemBo, PostResponseDto>()
			    .ForMember(postResponseDto => postResponseDto.Data, opt => opt.MapFrom(x => x));
		    this.CreateMap<PostsResponseBo, PostsResponseDto>()
			    .ForMember(postsResponseDto => postsResponseDto.Data, opt => opt.MapFrom(x => x.Posts));
	    }

	    private void ConfigureCommentMappers()
	    {
			this.CreateMap<CreateCommentRequestDto, CreateCommentRequestBo>();
		    this.CreateMap<CreateCommentRequestBo, CommentEntity>()
			    .ForMember(commentEntity => commentEntity.Id, opt => opt.MapFrom(x => ObjectId.GenerateNewId()))
				.ForMember(commentEntity => commentEntity.PostId, opt => opt.ResolveUsing((src, dst, arg3, context) => context.Options.Items["PostId"]))
			    .ForMember(commentEntity => commentEntity.ParentId, opt => opt.ResolveUsing((src, dst, arg3, context) => context.Options.Items["ParentId"]))
			    .ForMember(commentEntity => commentEntity.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow));

		    this.CreateMap<CommentEntity, CommentItemBo>();
		    this.CreateMap<CommentItemBo, CommentItemDto>();
		    this.CreateMap<CommentItemBo, CommentResponseDto>()
			    .ForMember(commentResponseDto => commentResponseDto.Data, opt => opt.MapFrom(x => x));
			this.CreateMap<CommentsResponseBo, CommentResponseDto>()
			    .ForMember(commentResponseDto => commentResponseDto.Data, opt => opt.MapFrom(x => x.Comments));
			this.CreateMap<CommentsResponseBo, CommentsResponseDto>()
			    .ForMember(commentsResponseDto => commentsResponseDto.Data, opt => opt.MapFrom(x => x.Comments));
	    }
	}
}
