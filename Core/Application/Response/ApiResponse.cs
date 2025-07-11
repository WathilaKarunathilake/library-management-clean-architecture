// <copyright file="ApiResponse.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Core.Application.Response
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; } = true;
    }
}
