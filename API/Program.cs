// <copyright file="Program.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

using LibraryManagementCleanArchitecture.API.Extensions;
using Serilog;

public class Program
{
   public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, services, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services
            .AddCustomCors()
            .AddCustomControllers()
            .AddCustomSwagger()
            .AddApplicationServices()
            .AddInfrastructureServices(builder.Configuration)
            .AddCustomAuthentication(builder.Configuration)
            .AddAuthorizationPolicies()
            .AddCustomValidation();

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.UseCors("AllowFrontend");
        app.UseExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.RegisterAllEndpointGroups();
        app.Run();
    }
}
