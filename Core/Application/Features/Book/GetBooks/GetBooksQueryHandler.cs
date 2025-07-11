// <copyright file="GetBooksQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using MediatR;

    public class GetBooksQueryHandler: IRequestHandler<GetBooksQuery, Result<List<BookDTO>>>
    {
        private IRepository<Book> bookRepository;
        private readonly IMapper mapper;

        public GetBooksQueryHandler(IRepository<Book> bookRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.bookRepository = bookRepository;
        }

        public async Task<Result<List<BookDTO>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await bookRepository.GetAllAsync();
            var bookDtos = mapper.Map<List<BookDTO>>(books);
            return Result<List<BookDTO>>.Success(bookDtos);
        }
    }
}
