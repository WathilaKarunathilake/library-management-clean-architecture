// <copyright file="GetBorrowedBooksByIdQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.GetBorrowedBoosById
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public class GetBorrowedBooksByIdQueryHandler : IRequestHandler<GetBorrowedBooksByIdQuery, Result<List<BookDTO>>>
    {
        private readonly IRepository<Borrowings> borrowingRepository;
        private readonly IRepository<Book> bookRepository;
        private readonly IMapper mapper;

        public GetBorrowedBooksByIdQueryHandler(
            IRepository<Borrowings> borrowingRepository,
            IRepository<Book> bookRepository,
            IMapper mapper)
        {
            this.borrowingRepository = borrowingRepository;
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<BookDTO>>> Handle(GetBorrowedBooksByIdQuery request, CancellationToken cancellationToken)
        {
            var borrowings = await this.borrowingRepository.GetAllAsync();

            var borrowedBookIds = borrowings
                .Where(b => b.MemberId == Guid.Parse(request.MemberId))
                .Select(b => b.BookId)
                .Distinct()
                .ToList();

            var books = await this.bookRepository.GetAllAsync();

            var borrowedBooks = books
                .Where(book => borrowedBookIds.Contains(book.BookId))
                .ToList();

            var bookDTOs = this.mapper.Map<List<BookDTO>>(borrowedBooks);
            return Result<List<BookDTO>>.Success(bookDTOs);
        }
    }

}
