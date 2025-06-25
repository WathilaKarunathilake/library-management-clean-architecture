using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result<Unit>>
    {
        private readonly IUnitOfWork unitOfWork;

        public BorrowBookCommandHandler(IUnitOfWork unitOfWork)
        {
                this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var member = await unitOfWork.Members.GetByIdAsync(request.MemberId);
            var book = await unitOfWork.Books.GetByIdAsync(request.BookId);

            if (member == null || book == null)
                return Result<Unit>.Failure("Book or Member not found.");

            if (!member.CanBorrow())
                return Result<Unit>.Failure("Member is not eligible to borrow books.");

            if (!book.Available)
                return Result<Unit>.Failure("Book is not available to borrow.");

            if (member is LibraryMember libraryMember)
            {
                book.Available = false;
                libraryMember.BooksBorrowed++;
            }

            await unitOfWork.Books.UpdateAsync(book);
            await unitOfWork.Members.UpdateAsync(member);
            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}