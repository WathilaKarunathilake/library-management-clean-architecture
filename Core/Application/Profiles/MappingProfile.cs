// <copyright file="MappingProfile.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Core.Application.Profiles
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using LibraryManagementCleanArchitecture.Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Member, MemberDTO>().ReverseMap();
            this.CreateMap<LibraryMember, MemberDTO>().ForMember(dest => dest.MemberType, opt => opt.MapFrom(src => "Library"));
            this.CreateMap<StaffMember, MemberDTO>().ForMember(dest => dest.MemberType, opt => opt.MapFrom(src => "Staff"));

            this.CreateMap<Book, BookDTO>().ReverseMap();
            this.CreateMap<AddBookCommand, Book>().ReverseMap();
        }
    }
}
