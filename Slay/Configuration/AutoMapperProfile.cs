using AutoMapper;
using Slay.Models.BusinessObjects.Post;
using Slay.Models.DataTransferObjects.Post;

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

            this.CreateMap<PostResponseBo, PostResponseDto>();
        }
    }
}
