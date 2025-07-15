// <copyright file="BorrowBookCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Domain.Errors;
    using MediatR;

    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result<Unit>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IRepository<Borrowings> borrowRepository;
        private readonly IUnitOfWork unitOfWork;

        public BorrowBookCommandHandler(IUnitOfWork unitOfWork, IRepository<Book> bookRepository, IRepository<Member> memberRepository, IRepository<Borrowings> borrowingRepository)
        {
                this.unitOfWork = unitOfWork;
                this.bookRepository = bookRepository;
                this.memberRepository = memberRepository;
                this.borrowRepository = borrowingRepository;
        }

        public async Task<Result<Unit>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var member = await this.memberRepository.GetByIdAsync(request.MemberId);
            var book = await this.bookRepository.GetByIdAsync(request.BookId);

            if (member == null)
            {
                return Result<Unit>.Failure(DomainErrors.Library.MemberNotFound());
            }
            else if (book == null)
            {
                return Result<Unit>.Failure(DomainErrors.Library.BookNotFound());
            }

            if (!member.CanBorrow())
            {
                return Result<Unit>.Failure(DomainErrors.Library.AccessDenied());
            }

            if (!book.Available)
            {
                return Result<Unit>.Failure(DomainErrors.Library.BookNotAvailableToBorrow());
            }

            if (member is LibraryMember libraryMember)
            {
                await this.borrowRepository.AddAsync(new Borrowings
                {
                    BookId = request.BookId,
                    MemberId = request.MemberId,
                });

                book.Available = false;
                libraryMember.BooksBorrowed++;
            }

            await this.bookRepository.UpdateAsync(book);
            await this.memberRepository.UpdateAsync(member);
            await this.unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
