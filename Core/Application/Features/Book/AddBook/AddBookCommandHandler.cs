﻿using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

public class AddBookCommandHandler: IRequestHandler<AddBookCommand, Result<BookDTO>>
{
    private IRepository<Book> bookRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AddBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.bookRepository = bookRepository;
    }

    public async Task<Result<BookDTO>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = this.mapper.Map<Book>(request);
        await this.bookRepository.AddAsync(book);
        await this.unitOfWork.SaveChangesAsync();

        var bookDto = this.mapper.Map<BookDTO>(book);
        return Result<BookDTO>.Success(bookDto);
    }
}
