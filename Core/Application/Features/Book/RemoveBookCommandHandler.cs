using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand, Unit>
    {
        private readonly IRepository<Book> repository;

        public RemoveBookCommandHandler(IRepository<Book> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await repository.GetByIdAsync(request.BookId);
            if (existingBook == null)
            {
                throw new ArgumentException("Book not found to remove");
            }

            await repository.DeleteAsync(request.BookId);
            await repository.SaveAsync();

            return Unit.Value;
        }
    }
}
