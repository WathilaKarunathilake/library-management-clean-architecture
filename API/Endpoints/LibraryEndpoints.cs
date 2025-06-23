using LibraryManagementCleanArchitecture.Application.Features.Library;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using MediatR;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public static class LibraryEndpoints
    {
        public static void MapLibraryEndpoints(this IEndpointRouteBuilder app)
        {
            var libraryGroup = app.MapGroup("/api/library").WithTags("Library Endpoints");

            libraryGroup.MapPost("/borrow/{bookId}/{memberId}", BorrowBook);
            libraryGroup.MapPost("/return/{bookId}/{memberId}", ReturnBook); 
        }

        private async static Task<IResult> BorrowBook(
            Guid bookId,
            Guid memberId,
            ISender sender)
        {
            try
            {
                 await sender.Send(new BorrowBookCommand
                    {
                        memberId = memberId,
                        bookId = bookId
                    });
                return Results.Ok(new ApiResponse<string>
                {
                    Data = "Book borrowed successfully !"
                });
            }
            catch (ArgumentException argEx)
            {
                return Results.BadRequest(new ApiResponse<string>
                {
                    Data = argEx.Message,
                    Success = false
                });
            }
            catch (KeyNotFoundException notFoundEx)
            {
                return Results.BadRequest(new ApiResponse<string>
                {
                    Data = notFoundEx.Message,
                    Success = false
                });
            }
        }

        private static async Task<IResult> ReturnBook(
            Guid bookId,
            Guid memberId,
            ISender sender)
        {
            try
            {
                await sender.Send(new ReturnBookCommand
                {
                    bookId = bookId,
                    memberId = memberId,
                });
                return Results.Ok(new ApiResponse<string>
                {
                    Data = "Book returned successfully !"
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
            catch (KeyNotFoundException error)
            {
                return Results.BadRequest(new ApiResponse<string>
                {
                    Data = error.Message,
                    Success = false
                });
            }
        }
    }
}
