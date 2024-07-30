using FluentValidation;

namespace OnlineBookShop.Application.App.Books.Commands
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .Length(3, 200);

            RuleFor(b => b.Description)
                .Length(5, int.MaxValue);

            RuleFor(b => b.PublishedOn)
                .NotEmpty();

            RuleFor(b => b.PublisherId)
                .NotEmpty();

            RuleFor(b => b.Price)
                .NotEmpty();
        }
    }
}
