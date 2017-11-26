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
				.ForMember(postEntity => postEntity.Comments, opt => opt.UseValue(Enumerable.Empty<CommentEntity>()));

	        this.CreateMap<PostEntity, PostResponseBo>();
			this.CreateMap<PostResponseBo, PostResponseDto>();
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
