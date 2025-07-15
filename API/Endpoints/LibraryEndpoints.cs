// <copyright file="LibraryEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    using LibraryManagementCleanArchitecture.API.Extensions;
    using LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook;
    using LibraryManagementCleanArchitecture.Application.Features.Library.GetBorrowedBoosById;
    using LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using LibraryManagementCleanArchitecture.Core.Application.Response;
    using MediatR;

    public class LibraryEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var libraryGroup = app.MapGroup("/api/library").WithTags("Library Endpoints");
            libraryGroup.RequireAuthorization("LIBRARY");

            libraryGroup.MapGet("/borrowings/{memberId}", GetBorrowingsByMemberId);
            libraryGroup.MapPost("/borrow", BorrowBook);
            libraryGroup.MapPost("/return", ReturnBook);
        }

        private static async Task<IResult> GetBorrowingsByMemberId(
            string memberId,
            ISender sender)
        {
            var result = await sender.Send(new GetBorrowedBooksByIdQuery { MemberId = memberId });
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

        private static async Task<IResult> BorrowBook(
            BorrowBookCommand borrowCommand,
            ISender sender)
        {
            var result = await sender.Send(borrowCommand);

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<string>
                {
                    Data = "Book borrowed successfully!",
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false,
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
