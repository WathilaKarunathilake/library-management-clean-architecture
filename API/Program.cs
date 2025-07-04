using FluentValidation;
using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.Behaviour;
using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Application.Contracts.Services;
using LibraryManagementCleanArchitecture.Application.Features.Books.AddBook;
using LibraryManagementCleanArchitecture.Core.Application.Profiles;
using LibraryManagementCleanArchitecture.Infastrcuture.Identity.Context;
using LibraryManagementCleanArchitecture.Infastrcuture.Identity.Models;
using LibraryManagementCleanArchitecture.Infastrcuture.Identity.Requirnments;
using LibraryManagementCleanArchitecture.Infastrcuture.Identity.Services;
using LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context;
using LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Context;
using LibraryManagementCleanArchitecture.Persistence.UoW;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Staff", policy => policy.RequireRole("STAFF"));
    options.AddPolicy("Library", policy => policy.RequireRole("LIBRARY"));
    options.AddPolicy("MinorOnly", policy =>
        policy.Requirements.Add(new StaffTypeRequirement("minor")));
    options.AddPolicy("MajorOnly", policy =>
        policy.Requirements.Add(new StaffTypeRequirement("major")));
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog();
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
});

builder.Services.AddValidatorsFromAssemblyContaining<AddBookCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtTokenGenerateService, JwtTokenGenerateService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors("AllowAll");

app.UseExceptionHandler();
app.UseAuthentication();   
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterAllEndpointGroups();
app.Run();