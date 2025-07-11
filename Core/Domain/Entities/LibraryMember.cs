// <copyright file="LibraryMember.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class LibraryMember : Member
    {
        private int booksBorrowed;

        public LibraryMember()
            : base()
        {
        }

        public LibraryMember(Guid id, string name)
            : base(id, name)
        {
            this.booksBorrowed = 0;
        }

        public int BooksBorrowed
        {
            get => this.booksBorrowed;
            set
            {
                if (value >= 0)
                {
                    this.booksBorrowed = value;
                }
                else
                {
                    throw new ArgumentException("Number of books borrowed cannot be negative.");
                }
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
            return $"\nName: {this.Name}\nType: Member\nBooks Borrowed: {this.booksBorrowed}\n";
        }
    }
}
