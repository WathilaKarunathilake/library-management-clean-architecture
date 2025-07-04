using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
using LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks;
using LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using MediatR;
namespace LibraryManagementCleanArchitecture.API.Endpoints;

public class BookEndpoints: IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var bookGroup = app.MapGroup("/api/books").WithTags("Book Endpoints");
        bookGroup.RequireAuthorization();

        bookGroup.MapPost("/", AddBook);
        bookGroup.MapGet("/", GetBooks);
        bookGroup.MapDelete("/{bookId}", RemoveBook);
    }

    private static async Task<IResult> GetBooks(Guid memberId, ISender sender)
    {
        var result = await sender.Send(new GetBooksQuery { MemberId = memberId });

        if (result.IsSuccess)
        {
            return Results.Ok(new ApiResponse<List<BookDTO>>
            {
                Data = result.Value,
                Success = true
            });
        }

        return Results.BadRequest(new ApiResponse<string>
        {
            Data = result.Error,
            Success = false
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
                Success = true
            });
        }

        return Results.BadRequest(new ApiResponse<string>
        {
            Data = result.Error,
            Success = false
        });
    }


    private static async Task<IResult> RemoveBook(Guid bookId, ISender sender)
    {
        var result = await sender.Send(new RemoveBookCommand { BookId = bookId });

        if (result.IsSuccess)
        {
            return Results.NoContent();
        }

        return Results.BadRequest(new ApiResponse<string>
        {
            Data = result.Error,
            Success = false
        });
    }
}
