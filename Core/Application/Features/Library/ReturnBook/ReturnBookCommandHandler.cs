// <copyright file="ReturnBookCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Domain.Errors;
    using MediatR;

    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result<Unit>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IRepository<Borrowings> borrowingRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookCommandHandler(IRepository<Borrowings> borrowingRepository, IUnitOfWork unitOfWork, IRepository<Book> bookRepository, IRepository<Member> memberRepository)
        {
            this.unitOfWork = unitOfWork;
            this.bookRepository = bookRepository;
            this.borrowingRepository = borrowingRepository;
            this.memberRepository = memberRepository;
        }

        public async Task<Result<Unit>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await this.bookRepository.GetByIdAsync(request.BookId);
            var member = await this.memberRepository.GetByIdAsync(request.MemberId);
            var borrowings = await this.borrowingRepository.GetAllAsync();

            if (member == null)
            {
                return Result<Unit>.Failure(DomainErrors.Library.MemberNotFound());
            }
            else if (book == null)
            {
                return Result<Unit>.Failure(DomainErrors.Library.BookNotFound());
            }

            var borrowing = borrowings
                .FirstOrDefault(b => b.BookId == request.BookId && b.MemberId == request.MemberId);

            if (borrowing == null)
            {
                return Result<Unit>.Failure(DomainErrors.Library.BookNotBorrowedByMember());
            }

            if (book.Available)
            {
                return Result<Unit>.Failure(DomainErrors.Library.BookAlreadyAvailable());
            }

            if (member is LibraryMember libraryMember)
            {
                book.Available = true;
                libraryMember.BooksBorrowed = Math.Max(0, libraryMember.BooksBorrowed - 1);

                await this.borrowingRepository.DeleteAsync(borrowing.BorrowingId);
            }

            await this.bookRepository.UpdateAsync(book);
            await this.memberRepository.UpdateAsync(member);
            await this.unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
