// <copyright file="DomainErrors.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Library
        {
            public static Error BookNotBorrowedByMember() =>
                Error.NotFound("Library.BookNotBorrowedByMember", $"The book is not borrowed by you");

            public static Error MemberNotFound() =>
                Error.NotFound("Library.MemberNotFound", "Member not found.");

            public static Error BookNotFound() =>
                Error.NotFound("Library.BookNotFound", "Book not found.");

            public static Error BookNotAvailableToBorrow() =>
                Error.NotFound("Library.BookNotAvailableToBorrow", "Book is not available to borrow at the moment.");

            public static Error AccessDenied() =>
                Error.NotFound("Library.AccessDenied", "You are not allowed to borrow books.");

            public static Error BookAlreadyAvailable() =>
               Error.NotFound("Library.BookAlreadyAvailable", "Book already availble.");
        }

        public static class Member
        {
            public static Error NoMembersFound() =>
               Error.NotFound("Member.NoMembersFound", "No members found.");
        }

        public static class Identity
        {
            public static Error InvalidCredentials =>
                Error.Unauthorized("Identity.InvalidCredentials", "The provided email or password is incorrect");

            public static Error RoleNotFound(string roleName) =>
                Error.NotFound("Identity.RoleNotFound", $"The role '{roleName}' does not exist");
        }

        public static class Book
        {
            public static Error BookBorrowedCannotDelete() => Error.Conflict("Book.BookBorrowedCannotDelete", "The book is already borrowed so cannot remove");

            public static Error AccessDenied() =>
               Error.Forbidden("Book.AccessDenied", "You do not have permission to access this resource");

            public static Error MemberNotFound(Guid memberId) =>
                Error.NotFound("Book.MemberNotFound", $"Member with {memberId} is not found");

            public static Error NotFound() =>
                Error.NotFound("Book.NotFound", $"Book not found");
        }

        public static class Custom
        {
            public static Error Failure(string error) => Error.Failure("Custom.Failure", error);
        }
    }
}
