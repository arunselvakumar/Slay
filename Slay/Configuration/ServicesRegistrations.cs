namespace Slay.Host.Configuration
{
    using FluentValidation;

    using Microsoft.Extensions.DependencyInjection;

    using Slay.Business.Services.Aggregators;
    using Slay.Business.Services.Facades;
    using Slay.Business.Services.Providers.ValidationsProviders;
    using Slay.Business.Services.Services;
    using Slay.Business.Services.Validators.Category;
    using Slay.Business.Services.Validators.Comment;
    using Slay.Business.Services.Validators.File;
    using Slay.Business.Services.Validators.Post;
    using Slay.Business.ServicesContracts.Aggregators;
    using Slay.Business.ServicesContracts.Facades;
    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.Dal.Repositories;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Post;

    public static class ServicesRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentAggregationService, CommentAggregationService>();
            services.AddScoped<ITemplateService, TemplateService>();

            services.AddScoped<IAzureStorageServicesFacade, AzureStorageServicesFacade>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IValidationsProvider, ValidationsProvider>();

            services.AddTransient<IValidator<CreatePostRequestBo>, CreatePostValidator>();
            services.AddTransient<IValidator<CreateCommentRequestBo>, CreateCommentValidator>();
            services.AddTransient<IValidator<CreateCategoryRequestBo>, CreateCategoryValidator>();
            services.AddTransient<IValidator<PostUploadRequestContext>, PostUploadRequestValidator>();
            services.AddTransient<IValidator<TemplateUploadRequestContext>, TemplateUploadRequestValidator>();
        }
    }
}