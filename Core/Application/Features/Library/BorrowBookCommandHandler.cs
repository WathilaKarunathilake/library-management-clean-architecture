using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Library
{
    public class BorrowBookCommandHandler: IRequestHandler<BorrowBookCommand, Unit>
    {
        private readonly IRepository<Book> bookRepositroy;
        private readonly IRepository<Member> memberRepository;

        public BorrowBookCommandHandler(IRepository<Book> bookRepositroy, IRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
            this.bookRepositroy = bookRepositroy;
        }

        public async Task<Unit> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            Member member = await memberRepository.GetByIdAsync(request.memberId);
            Book book = await bookRepositroy.GetByIdAsync(request.bookId);

            if (!member.CanBorrow())
                throw new ArgumentException("Member is not eligible to borrow books.");

            if (!book.Available)
                throw new ArgumentException("Book is not available to borrow.");

            if (member is LibraryMember libraryMember)
            {
                book.Available = false;
                libraryMember.BooksBorrowed++;
            }

            await bookRepositroy.UpdateAsync(book);
            await memberRepository.UpdateAsync(member);
            await bookRepositroy.SaveAsync();
            await memberRepository.SaveAsync();

            return Unit.Value;  
        }
    }
}
