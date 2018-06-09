﻿namespace Slay.Business.ServicesContracts.Providers.ValidationsProviders
{
    using FluentValidation;

    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.Post;

    public interface IValidationsProvider
    {
        IValidator<CreatePostRequestBo> CreatePostValidator { get; }

        IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }
    }
}