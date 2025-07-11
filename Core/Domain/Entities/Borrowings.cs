// <copyright file="Borrowings.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class Borrowings
    {
        public Guid BorrowingId { get; }

        public Guid MemberId { get; set; }

        public Guid BookId { get; set; }
    }
}
