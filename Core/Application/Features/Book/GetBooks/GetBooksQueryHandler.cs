using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using LibraryManagementCleanArchitecture.Domain.Errors;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks
{
    public class GetBooksQueryHandler: IRequestHandler<GetBooksQuery, Result<List<BookDTO>>>
    {
        private IRepository<Member> memberRepository;
        private IRepository<Book> bookRepository;
        private readonly IMapper mapper;

        public GetBooksQueryHandler(IRepository<Member> memberRepository, IRepository<Book> bookRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.memberRepository = memberRepository;
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
