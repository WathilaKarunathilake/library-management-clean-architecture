using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result<Unit>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookCommandHandler(IUnitOfWork unitOfWork, IRepository<Book> bookRepository, IRepository<Member> memberRepository)
        {
            this.unitOfWork = unitOfWork;
            this.bookRepository = bookRepository;
            this.memberRepository = memberRepository;
        }

        public async Task<Result<Unit>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.BookId);
            var member = await memberRepository.GetByIdAsync(request.MemberId);

            if (book == null || member == null)
                return Result<Unit>.Failure("Book or Member not found.");

            if (book.Available)
                return Result<Unit>.Failure("Book is already available.");

            if (member is LibraryMember libraryMember)
            {
                book.Available = true;
                libraryMember.BooksBorrowed = Math.Max(0, libraryMember.BooksBorrowed - 1);
            }

            await bookRepository.UpdateAsync(book);
            await memberRepository.UpdateAsync(member);
            await unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
