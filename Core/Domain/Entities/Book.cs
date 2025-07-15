// <copyright file="Book.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class Book
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

        private Guid bookId;
        private string? title;
        private string? author;
        private string? description;
        private string? category;
        private int publicationYear;
        private bool available;

        public Guid BookId => this.bookId;

        public string? Title
        {
            get => this.title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be empty.");
                }

                this.title = value;
            }
        }

        public string? Description
        {
            get => this.description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be empty.");
                }
                else if (value.Length < 20)
                {
                    throw new ArgumentException("Description must be at least 20 characters.");
                }

                this.description = value;
            }
        }

        public string? Author
        {
            get => this.author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Author cannot be empty.");
                }

                this.author = value;
            }
        }

        public int PublicationYear
        {
            get => this.publicationYear;
            set
            {
                if (value < 1500 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException($"Publication year must be between 1500 and {DateTime.Now.Year}.");
                }

                this.publicationYear = value;
            }
        }

        public string? Category
        {
            get => this.category;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Category cannot be empty.");
                }

                if (!AllowedCategories.Contains(value))
                {
                    throw new ArgumentException($"Invalid category. Allowed categories: {string.Join(", ", AllowedCategories)}");
                }

                this.category = value;
            }
        }

        public bool Available
        {
            get => this.available;
            set => this.available = value;
        }

        public Book() => this.Available = true;

        public Book(string title, string author, int publicationYear, string category, string description)
        {
            this.Title = title;
            this.Author = author;
            this.PublicationYear = publicationYear;
            this.Category = category;
            this.Description = description;
            this.Available = true;
        }

        public override string ToString()
        {
            return $"\nTitle: {this.Title}\nAuthor: {this.Author}\nPublication Year: {this.PublicationYear}\nCategory: {this.Category}\nAvailability: {this.Available}\n";
        }
    }
}
