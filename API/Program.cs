using LibraryManagementCleanArchitecture.API.Endpoints;
using LibraryManagementCleanArchitecture.Application.Features.Books;
using LibraryManagementCleanArchitecture.Application.Features.Library;
using LibraryManagementCleanArchitecture.Application.Features.Members;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Core.Application.Profiles;
using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context;
using LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddBookCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RemoveBookCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetBooksQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetBookByIdQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RemoveBookCommand).Assembly);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBookEndpoints();
app.MapLibraryEndpoints();
app.MapMemberEndpoints();

app.Run();