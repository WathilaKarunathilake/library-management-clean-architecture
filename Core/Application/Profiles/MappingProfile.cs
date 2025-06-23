using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Features.Books;
using LibraryManagementCleanArchitecture.Application.Features.Members;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;

namespace LibraryManagementCleanArchitecture.Core.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Member, MemberDTO>().ReverseMap();

            CreateMap<AddBookCommand, Book>();
            CreateMap<AddMemberCommand, Member>();
        }
    }
}
