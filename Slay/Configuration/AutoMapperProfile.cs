using AutoMapper;
using Slay.BusinessObjects.Post;
using Slay.DataTransferObjects.Post;

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
