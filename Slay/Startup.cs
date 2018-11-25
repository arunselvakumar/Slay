namespace Slay.Host
{
    using AutoMapper;

    using FluentValidation.AspNetCore;

    using IdentityServer4.AccessTokenValidation;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Slay.Host.Configuration;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; })
                    .AddFluentValidation();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options => { options.Authority = "http://localhost:50365/"; options.RequireHttpsMetadata = false; options.ApiName = "socialnetwork_fullaccess"; options.ApiSecret = "secret"; });

            services.AddAutoMapper();

            services.RegisterServices();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Slay Project", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            
            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseAuthentication();

            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}"); });
        }
    }
}