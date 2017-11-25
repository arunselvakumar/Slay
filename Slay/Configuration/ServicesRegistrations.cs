using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Slay.Dal.Repositories;
using Slay.DalContracts.Repositories;
using Slay.Models.BusinessObjects.Post;
using Slay.Services.Providers.ValidationsProviders;
using Slay.Services.Services;
using Slay.Services.Validators.Post;
using Slay.ServicesContracts.Providers.ValidationsProviders;
using Slay.ServicesContracts.Services;

namespace Slay.Host.Configuration
{
	public static class ServicesRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
	        services.AddScoped<IPostRepository, PostRepository>();
	        services.AddScoped<IPostValidationsProvider, PostValidationsProvider>();

	        services.AddTransient<IValidator<CreatePostRequestBo>, CreatePostValidator>();
		}
    }
}
