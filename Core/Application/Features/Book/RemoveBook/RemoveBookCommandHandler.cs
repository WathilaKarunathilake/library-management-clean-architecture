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
        private readonly IUnitOfWork unitOfWork;  
       
        public RemoveBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await bookRepository.GetByIdAsync(request.BookId);
            if (existingBook == null)
                return Result<Unit>.Failure(DomainErrors.Book.NotFound());

            await bookRepository.DeleteAsync(request.BookId);
            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
