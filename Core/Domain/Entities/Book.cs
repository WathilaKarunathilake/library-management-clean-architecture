using System;

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class Book
    {
        private Guid bookId;
        private string? title;
        private string? author;
        private string? category;
        private int publicationYear;
        private bool available;

        public Guid BookId{ get { return bookId; } }

        public string? Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty.");
                title = value;
            }
        }

        public string? Author
        {
            get => author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author name cannot be empty.");
                author = value;
            }
        }

        public int PublicationYear
        {
            get => publicationYear;
            set
            {
                if (value <= 0 || value > DateTime.Now.Year + 1) 
                    throw new ArgumentException("Invalid publication year.");
                publicationYear = value;
            }
        }

        public string? Category
        {
            get => category;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Category cannot be empty.");
                category = value;
            }
        }

        public bool Available
        {
            get => available;
            set => available = value;
        }

        public Book()
        {
            Available = true;
        }

        public Book(string title, string author, int publicationYear, string category)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            Category = category;
            Available = true;
        }

        public override string ToString()
        {
            return $"\nTitle: {Title}\nAuthor: {Author}\nPublication Year: {PublicationYear}\nCategory: {Category}\nAvailability: {Available}\n";
        }
    }
}
