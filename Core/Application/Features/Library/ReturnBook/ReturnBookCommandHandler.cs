using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result<Unit>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.Books.GetByIdAsync(request.BookId);
            var member = await unitOfWork.Members.GetByIdAsync(request.MemberId);

            if (book == null || member == null)
                return Result<Unit>.Failure("Book or Member not found.");

            if (book.Available)
                return Result<Unit>.Failure("Book is already available.");

            if (member is LibraryMember libraryMember)
            {
                book.Available = true;
                libraryMember.BooksBorrowed = Math.Max(0, libraryMember.BooksBorrowed - 1);
            }

            await unitOfWork.Books.UpdateAsync(book);
            await unitOfWork.Members.UpdateAsync(member);
            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
