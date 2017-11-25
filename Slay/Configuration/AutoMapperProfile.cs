using AutoMapper;
using Slay.Models.BusinessObjects.Post;
using Slay.Models.DataTransferObjects.Post;
using Slay.Models.Entities;

namespace Slay.Host.Configuration
{
	public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.ConfigurePostMappers();
        }

        private void ConfigurePostMappers()
        {
	        this.CreateMap<CreatePostRequestDto, CreatePostRequestBo>();
	        this.CreateMap<CreatePostRequestBo, PostEntity>();

	        this.CreateMap<PostEntity, PostResponseBo>();
			this.CreateMap<PostResponseBo, PostResponseDto>();
		}
    }
}
