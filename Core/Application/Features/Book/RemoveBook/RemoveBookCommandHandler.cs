using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    public class RemoveBookCommandHandler: IRequestHandler<RemoveBookCommand, Result<Unit>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveBookCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await unitOfWork.Books.GetByIdAsync(request.BookId);
            if (existingBook == null)
                return Result<Unit>.Failure("Book not found to remove.");

            await unitOfWork.Books.DeleteAsync(request.BookId);
            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
