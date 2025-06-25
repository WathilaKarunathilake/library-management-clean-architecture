using AutoMapper;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks
{
    using LibraryManagementCleanArchitecture.Application.Interfaces;
    using LibraryManagementCleanArchitecture.Application.Response;
    using MediatR;

    public class GetBooksQueryHandler: IRequestHandler<GetBooksQuery, Result<List<BookDTO>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<List<BookDTO>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var member = await unitOfWork.Members.GetByIdAsync(request.MemberId);
            if (member == null)
                return Result<List<BookDTO>>.Failure("Member ID does not exist.");

            if (!member.CanViewBooks())
                return Result<List<BookDTO>>.Failure("You are not authorized to view books.");

            var books = await unitOfWork.Books.GetAllAsync();
            var bookDtos = mapper.Map<List<BookDTO>>(books);
            return Result<List<BookDTO>>.Success(bookDtos);
        }
    }

}
