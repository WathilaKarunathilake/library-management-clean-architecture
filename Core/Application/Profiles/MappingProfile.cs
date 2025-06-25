using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;

namespace LibraryManagementCleanArchitecture.Core.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<LibraryMember, MemberDTO>().ForMember(dest => dest.MemberType, opt => opt.MapFrom(src => "Library")); ;
            CreateMap<StaffMember, MemberDTO>().ForMember(dest => dest.MemberType, opt => opt.MapFrom(src => "Staff"));

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<AddBookCommand, Book>().ReverseMap();
        }
    }
}
