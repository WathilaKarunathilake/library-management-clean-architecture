using AutoMapper;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class AddBookCommandHandler: IRequestHandler<AddBookCommand, BookDTO>
    {
        private readonly IRepository<Book> repository;
        private readonly IMapper mapper;

        public AddBookCommandHandler(IRepository<Book> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BookDTO> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = mapper.Map<Book>(request);
            await repository.AddAsync(book);
            await repository.SaveAsync();

            return mapper.Map<BookDTO>(book);
        }
    }
}
