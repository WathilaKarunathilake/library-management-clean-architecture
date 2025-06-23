using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Library
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Unit>
    {
        private readonly IRepository<Book> bookRepositroy;
        private readonly IRepository<Member> memberRepository;

        public ReturnBookCommandHandler(IRepository<Book> bookRepositroy, IRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
            this.bookRepositroy = bookRepositroy;
        }

        public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            Member member = await memberRepository.GetByIdAsync(request.memberId);
            Book book = await bookRepositroy.GetByIdAsync(request.bookId);

            if (book.Available)
                throw new ArgumentException("Book is already available.");

            if (member is LibraryMember libraryMember)
            {
                book.Available = true;
                libraryMember.BooksBorrowed = Math.Max(0, libraryMember.BooksBorrowed - 1);
            }

            await bookRepositroy.UpdateAsync(book);
            await memberRepository.UpdateAsync(member);
            await bookRepositroy.SaveAsync();
            await memberRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
