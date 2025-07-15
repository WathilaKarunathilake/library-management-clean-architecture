// <copyright file="RemoveBookCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Domain.Errors;
    using MediatR;

    public class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand, Result<Unit>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Borrowings> borrowingRepository;
        private readonly IUnitOfWork unitOfWork;

        public RemoveBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork, IRepository<Borrowings> borrowingRepository)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
            this.borrowingRepository = borrowingRepository;
        }

        public async Task<Result<Unit>> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await this.bookRepository.GetByIdAsync(request.BookId);
            if (existingBook == null)
            {
                return Result<Unit>.Failure(DomainErrors.Book.NotFound());
            }

            var borrowings = await this.borrowingRepository.GetAllAsync();
            var relatedBorrowings = borrowings.Where(b => b.BookId == request.BookId).ToList();
            if (relatedBorrowings.Any())
            {
                return Result<Unit>.Failure(DomainErrors.Book.BookBorrowedCannotDelete());
            }

            await this.bookRepository.DeleteAsync(request.BookId);
            await this.unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
