using System;

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class LibraryMember: Member
    {
        private int booksBorrowed;

        public LibraryMember() : base()
        {

        }

        public LibraryMember(string name)
            : base(name)
        {
            booksBorrowed = 0;
        }

        public int BooksBorrowed
        {
            get => booksBorrowed;
            set
            {
                if (value >= 0)
                    booksBorrowed = value;
                else
                    throw new ArgumentException("Number of books borrowed cannot be negative.");
            }
        }

        public override bool CanBorrow()
        {
            return true;
        }

        public override bool CanViewBooks()
        {
            return true;
        }

        public override string ToString()
        {
            return $"\nName: {Name}\nType: Member\nBooks Borrowed: {booksBorrowed}\n";
        }
    }
}
