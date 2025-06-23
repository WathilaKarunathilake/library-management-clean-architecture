using LibraryManagementCleanArchitecture.Application.Features.Books;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementCleanArchitecture.API.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        var bookGroup = app.MapGroup("/api/books").WithTags("Book Endpoints");

        bookGroup.MapGet("/{memberId}", GetBooks);
        bookGroup.MapPost("/", AddBook);
        bookGroup.MapDelete("/{bookId}", RemoveBook);
    }

    private static async Task<IResult> GetBooks(Guid memberId, ISender sender)
    {
        try
        {
            var books = await sender.Send(new GetBooksQuery
            {
                memberId = memberId
            });

            return Results.Ok(new ApiResponse<List<BookDTO>>
            {
                Data = books
            });
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(new ApiResponse<string>
            {
                Data = error.Message,
                Success = false
            });
        }
        catch (KeyNotFoundException key)
        {
            return Results.NotFound(new ApiResponse<string>
            {
                Data = key.Message,
                Success = false
            });
        }
    }

    private static async Task<IResult> AddBook(AddBookCommand addBookCommand, ISender sender)
    {
        try
        {
            var addedBook = await sender.Send(addBookCommand);
            return Results.Ok(new ApiResponse<BookDTO>
            {
                Data = addedBook
            });
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(new ApiResponse<string>
            {
                Data = error.Message,
                Success = false
            });
        }
    }

    private static async Task<IResult> RemoveBook(Guid bookId, ISender sender)
    {
        try
        {
            await sender.Send(new RemoveBookCommand { BookId = bookId });
            return Results.NoContent();
        }
        catch (Exception error)
        {
            return Results.BadRequest(new ApiResponse<string>
            {
                Data = error.Message,
                Success = false
            });
        }
    }
}
