using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Entities;
using LibraryManagementCleanArchitecture.Domain.Errors;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    public class RemoveBookCommandHandler: IRequestHandler<RemoveBookCommand, Result<Unit>>
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
            var existingBook = await bookRepository.GetByIdAsync(request.BookId);
            if (existingBook == null)
                return Result<Unit>.Failure(DomainErrors.Book.NotFound());

            var borrowings = await borrowingRepository.GetAllAsync();
            var relatedBorrowings = borrowings.Where(b => b.BookId == request.BookId).ToList();

            foreach (var borrowing in relatedBorrowings)
            {
                await borrowingRepository.DeleteAsync(borrowing.BorrowingId);
            }

            await bookRepository.DeleteAsync(request.BookId);
            await unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
