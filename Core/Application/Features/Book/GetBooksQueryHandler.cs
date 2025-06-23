using AutoMapper;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDTO>>
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public GetBooksQueryHandler(IRepository<Book> bookRepository, IRepository<Member> memberRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        public async Task<List<BookDTO>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            Member member = await memberRepository.GetByIdAsync(request.memberId);
            if (member == null)
            {
                throw new ArgumentException("Member ID doesnt exist");
            }

            if (!member.CanViewBooks())
            {
                throw new ArgumentException("You are not authorized to view books.");
            }

            var books = await bookRepository.GetAllAsync();
            return mapper.Map<List<BookDTO>>(books);
        }
    } 
}
