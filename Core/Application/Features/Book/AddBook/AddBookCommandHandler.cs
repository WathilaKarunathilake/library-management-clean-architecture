using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

public class AddBookCommandHandler: IRequestHandler<AddBookCommand, Result<BookDTO>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AddBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<BookDTO>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(request);
        await unitOfWork.Books.AddAsync(book);
        await unitOfWork.SaveChangesAsync();

        var bookDto = mapper.Map<BookDTO>(book);
        return Result<BookDTO>.Success(bookDto);
    }
}
