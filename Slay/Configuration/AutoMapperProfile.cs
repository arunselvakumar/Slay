using AutoMapper;
using Slay.Models.BOs.Post;
using Slay.Models.DTOs.Post;

namespace Slay.Configuration
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
