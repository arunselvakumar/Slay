using Microsoft.Extensions.DependencyInjection;
using Slay.Services;
using Slay.Services.Interfaces;

namespace Slay.Configuration
{
    public static class ServicesRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
        }
    }
}
