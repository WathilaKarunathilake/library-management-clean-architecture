// <copyright file="BookEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
    using LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks;
    using LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using LibraryManagementCleanArchitecture.Core.Application.Response;
    using MediatR;

    public class BookEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var bookGroup = app.MapGroup("/api/books").WithTags("Book Endpoints");

            bookGroup.MapPost("/", AddBook).RequireAuthorization("Staff");
            bookGroup.MapGet("/", GetBooks).RequireAuthorization();
            bookGroup.MapDelete("/{bookId}", RemoveBook).RequireAuthorization("Staff");
        }

        private static async Task<IResult> GetBooks(ISender sender)
        {
            var result = await sender.Send(new GetBooksQuery());

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<List<BookDTO>>
                {
                    Data = result.Value,
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false,
            });
        }

        private static async Task<IResult> AddBook(AddBookCommand addBookCommand, ISender sender)
        {
            var result = await sender.Send(addBookCommand);

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<BookDTO>
                {
                    Data = result.Value,
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false,
            });
        }

        private static async Task<IResult> RemoveBook(Guid bookId, ISender sender)
        {
            var result = await sender.Send(new RemoveBookCommand { BookId = bookId });

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<string>
                {
                    Data = "Removed successfully !",
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false,
            });
        }
    }
}
