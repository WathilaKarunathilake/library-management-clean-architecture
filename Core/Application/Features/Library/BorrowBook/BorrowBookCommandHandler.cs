using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result<Unit>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IUnitOfWork unitOfWork;

        public BorrowBookCommandHandler(IUnitOfWork unitOfWork, IRepository<Book> bookRepository, IRepository<Member> memberRepository)
        {
                this.unitOfWork = unitOfWork;
                this.bookRepository = bookRepository;
                this.memberRepository = memberRepository;
        }

        public async Task<Result<Unit>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var member = await memberRepository.GetByIdAsync(request.MemberId);
            var book = await bookRepository.GetByIdAsync(request.BookId);

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

            await bookRepository.UpdateAsync(book);
            await memberRepository.UpdateAsync(member);
            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}