// <copyright file="UserResultDTO.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.DTO
{
    public class UserResultDTO
    {
        public bool Succeeded { get; set; }

        public string? Name { get; set; }

        public string? Errors { get; set; }

        public Guid UserId { get; set; }

        public string? Role { get; set; }

        public string? Email { get; set; }
    }
}
