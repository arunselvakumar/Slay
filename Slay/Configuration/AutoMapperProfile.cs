namespace Slay.Host.Configuration
{
    using System;

    using AutoMapper;

    using Microsoft.WindowsAzure.Storage.Blob;

    using MongoDB.Bson;

    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Post;
    using Slay.Models.DataTransferObjects.Category;
    using Slay.Models.DataTransferObjects.Comment;
    using Slay.Models.DataTransferObjects.Post.Request;
    using Slay.Models.DataTransferObjects.Post.Response;
    using Slay.Models.Entities;
    using Slay.Models.Enums;
    using Slay.Utilities.Extensions;

    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.ConfigurePostMappers();
            this.ConfigurePostCategoryMappers();
            this.ConfigureCommentMappers();
            this.ConfigureFileMappers();
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

            this.CreateMap<PostItemBo, PostDto>();
            this.CreateMap<PostItemBo, PostResponseDto>()
                .ForMember(postResponseDto => postResponseDto.Data, opt => opt.MapFrom(x => x))
                .ForMember(postResponseDto => postResponseDto.TimeStamp, opt => opt.MapFrom(x => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()))
                .ForMember(postResponseDto => postResponseDto.Links, opt => opt.Ignore());

            this.CreateMap<PostsListResponseBo, PostsListResponseDto>()
                .ForMember(postsResponseDto => postsResponseDto.Data, opt => opt.MapFrom(x => x.Posts));
        }

        private void ConfigurePostCategoryMappers()
        {
            this.CreateMap<CreateCategoryRequestDto, CreateCategoryRequestBo>();
            this.CreateMap<CreateCategoryRequestBo, CategoryEntity>()
                .ForMember(categoryEntity => categoryEntity.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(categoryEntity => categoryEntity.ModifiedOn, opt => opt.MapFrom(x => DateTime.UtcNow));

            this.CreateMap<CategoryEntity, CategoryItemBo>();

            this.CreateMap<CategoryItemBo, CategoryItemDto>();
            this.CreateMap<CategoriesListResponseBo, CategoriesListResponseDto>().ForMember(
                categoriesListResponseDto => categoriesListResponseDto.Data, opt => opt.MapFrom(x => x.Categories));

            this.CreateMap<CreateCategoryResponseBo, CreateCategoryResponseDto>();
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
                .ForMember(commentResponseDto => commentResponseDto.Data, opt => opt.MapFrom(x => x))
                .ForMember(commentResponseDto => commentResponseDto.Links, opt => opt.Ignore());

            this.CreateMap<CommentsListResponseBo, CommentsListResponseDto>()
                .ForMember(commentResponseDto => commentResponseDto.Data, opt => opt.MapFrom(x => x.Comments))
                .ForMember(commentResponseDto => commentResponseDto.Links, opt => opt.Ignore());
        }

        private void ConfigureFileMappers()
        {
            this.CreateMap<CloudBlockBlob, PostUploadResponseContext>()
                .ForMember(fileUploadResponseContext => fileUploadResponseContext.Url, opt => opt.MapFrom(x => x.Uri.AbsoluteUri))
                .ForMember(fileUploadResponseContext => fileUploadResponseContext.PrimaryUrl, opt => opt.MapFrom(x => x.StorageUri.PrimaryUri.AbsoluteUri))
                .ForMember(fileUploadResponseContext => fileUploadResponseContext.SecondaryUrl, opt => opt.MapFrom(x => x.StorageUri.SecondaryUri.AbsoluteUri));

            this.CreateMap<CloudBlockBlob, TemplateEntity>()
                .ForMember(templateEntity => templateEntity.Id, opt => opt.MapFrom(x => ObjectId.GenerateNewId()))
                .ForMember(templateEntity => templateEntity.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(templateEntity => templateEntity.PrimaryUrl, opt => opt.MapFrom(x => x.StorageUri.PrimaryUri.AbsoluteUri))
                .ForMember(templateEntity => templateEntity.SecondaryUrl, opt => opt.MapFrom(x => x.StorageUri.SecondaryUri.AbsoluteUri));
        }
    }
}