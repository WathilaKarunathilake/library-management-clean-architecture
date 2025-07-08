// <copyright file="ServiceCollectionExtensions.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API.Extensions
{
    using System.Text;
    using FluentValidation;
    using global::LibraryManagementCleanArchitecture.Application.Behaviour;
    using global::LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using global::LibraryManagementCleanArchitecture.Application.Contracts.Services;
    using global::LibraryManagementCleanArchitecture.Core.Application.Profiles;
    using global::LibraryManagementCleanArchitecture.Infastrcuture.Identity.Context;
    using global::LibraryManagementCleanArchitecture.Infastrcuture.Identity.Models;
    using global::LibraryManagementCleanArchitecture.Infastrcuture.Identity.Requirnments;
    using global::LibraryManagementCleanArchitecture.Infastrcuture.Identity.Services;
    using global::LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context;
    using global::LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Context;
    using global::LibraryManagementCleanArchitecture.Persistence.UoW;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly);
            });
            return services;
        }

        public static IServiceCollection AddCustomValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<MappingProfile>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenGenerateService, JwtTokenGenerateService>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var keyString = jwtSettings.GetValue<string>("Key");

            var key = Encoding.UTF8.GetBytes(keyString);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                    };
                });

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Staff", policy => policy.RequireRole("STAFF"));
                options.AddPolicy("Library", policy => policy.RequireRole("LIBRARY"));
                options.AddPolicy("ManagementOnly", policy =>
                    policy.Requirements.Add(new StaffTypeRequirement("MANAGEMENT")));
            });

            services.AddScoped<IAuthorizationHandler, StaffTypeHandler>();

            return services;
        }
    }
}
