using AutoMapper;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class GetBooksByIdQueryHandler: IRequestHandler<GetBookByIdQuery, BookDTO>
    {
        private readonly IRepository<Book> repository;
        private readonly IMapper mapper;

        public GetBooksByIdQueryHandler(IRepository<Book> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BookDTO> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID '{request.BookId}' was not found.");
            }
            return mapper.Map<BookDTO>(book);
        }
    }
}
