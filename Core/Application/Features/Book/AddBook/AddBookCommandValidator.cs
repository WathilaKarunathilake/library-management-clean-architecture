using FluentValidation;
using System;
using System.Collections.Generic;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        private static readonly List<string> AllowedCategories = new ()
        {
            "Adventure",
            "Fantasy",
            "Science Fiction",
            "Mystery",
            "Romance",
            "Horror",
            "Biography",
            "History",
            "Self-Help",
            "Children",
        };

        public AddBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Must(category => AllowedCategories.Contains(category))
                .WithMessage($"Category must be one of the following: {string.Join(", ", AllowedCategories)}");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(20).WithMessage("Description must be at least 20 characters.")
                .MaximumLength(500).WithMessage("Description must be at maximum 500 characters.");

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1500, DateTime.UtcNow.Year)
                .WithMessage($"PublicationYear must be between 1500 and {DateTime.UtcNow.Year}");
        }
    }
}
