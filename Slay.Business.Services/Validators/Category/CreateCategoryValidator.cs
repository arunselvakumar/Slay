namespace Slay.Business.Services.Validators.Category
{
    using FluentValidation;

    using Slay.Models.BusinessObjects.Category;

    public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryRequestBo>
    {
        private readonly string _categoryNameShouldNotBeEmptyError = "CATEGORYNAME_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _categoryNameShouldBeLessThan99CharactersError = "CATEGORYNAME_SHOULDBELESSTHAN99CHARACTERS_ERROR";

        public CreateCategoryValidator()
        {
            this.RuleFor(request => request.Name).NotEmpty().WithMessage(this._categoryNameShouldNotBeEmptyError)
                .Length(1, 99).WithMessage(this._categoryNameShouldBeLessThan99CharactersError);
        }
    }
}