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
			    .ForMember(commentEntity => commentEntity.Comments, opt => opt.UseValue(Enumerable.Empty<CommentEntity>()))
			    .ForMember(commentEntity => commentEntity.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow));

		    this.CreateMap<CommentEntity, CommentResponseBo>();
		    this.CreateMap<CommentResponseBo, CommentResponseDto>();
	    }
	}
}
