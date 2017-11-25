using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Slay.Models.BusinessObjects.Post;
using Slay.Services.Services;
using Slay.Services.Validators.Post;
using Slay.ServicesContracts.Services;

namespace Slay.Host.Configuration
{
	public static class ServicesRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();

	        services.AddTransient<IValidator<CreatePostRequestBo>, CreatePostValidator>();
		}
    }
}
