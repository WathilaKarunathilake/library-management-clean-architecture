using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook;
using LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using MediatR;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public class LibraryEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var libraryGroup = app.MapGroup("/api/library").WithTags("Library Endpoints");

            libraryGroup.MapPost("/borrow/{bookId}/{memberId}", BorrowBook);
            libraryGroup.MapPost("/return/{bookId}/{memberId}", ReturnBook);
        }

        private async static Task<IResult> BorrowBook(
            BorrowBookCommand borrowCommand,
            ISender sender)
        {
            var result = await sender.Send(borrowCommand);

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<string>
                {
                    Data = "Book borrowed successfully!",
                    Success = true
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false
            });
        }

        private static async Task<IResult> ReturnBook(
             ReturnBookCommand returnBookCommand,
             ISender sender)
        {
            var result = await sender.Send(returnBookCommand);

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<string>
                {
                    Data = "Book returned successfully!",
                    Success = true
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false
            });
        }
    }
}
