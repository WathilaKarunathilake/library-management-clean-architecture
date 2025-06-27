using FluentValidation;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1500, DateTime.UtcNow.Year)
                .WithMessage($"PublicationYear must be between 1500 and {DateTime.UtcNow.Year}");
        }
    }
}
