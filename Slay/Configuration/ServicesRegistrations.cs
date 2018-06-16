namespace Slay.Host.Configuration
{
    using FluentValidation;

    using Microsoft.Extensions.DependencyInjection;

    using Slay.Business.Services.Aggregators;
    using Slay.Business.Services.Providers.ValidationsProviders;
    using Slay.Business.Services.Services;
    using Slay.Business.Services.Validators.Comment;
    using Slay.Business.Services.Validators.Post;
    using Slay.Business.ServicesContracts.Aggregators;
    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.Dal.Repositories;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.Post;

    public static class ServicesRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentAggregationService, CommentAggregationService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IValidationsProvider, ValidationsProvider>();

            services.AddTransient<IValidator<CreatePostRequestBo>, CreatePostValidator>();
            services.AddTransient<IValidator<CreateCommentRequestBo>, CreateCommentValidator>();
        }
    }
}