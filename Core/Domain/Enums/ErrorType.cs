// <copyright file="ErrorType.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Enums
{
    public enum ErrorType
    {
        Failure,
        Validation,
        Unauthorized,
        NotFound,
        None,
        Forbidden,
        Conflict,
    }
}
