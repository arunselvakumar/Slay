using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Slay.Host.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace Slay.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
					.AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; })
				    .AddFluentValidation();

	        services.AddAutoMapper();

            services.RegisterServices();

	        services.AddSwaggerGen(c =>
	        {
		        c.SwaggerDoc("v1", new Info { Title = "Slay Project", Version = "v1" });
	        });
		}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

	        app.UseSwagger();

	        app.UseSwaggerUI(c =>
	        {
		        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	        });

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
