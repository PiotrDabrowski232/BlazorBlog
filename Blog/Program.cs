using Blog.Data;
using Blog.Data.Data;
using Blog.Logic.Services.Interfaces;
using Blog.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Repositories.Interfaces;
using Blog.Data.Repositories;
using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Blog.Logic.Dto.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Blog.Logic.Dto.UserDtos;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Blog.Logic.Authentication;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers().AddFluentValidation();


//Authentication 
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<UserSession>();




// Dependency Injections Reposiotries
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleReposiotry>();
builder.Services.AddScoped<IPostRepository, PostRepository>();



// Dependency Injections Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connectionString)
.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));



//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Radzen
builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
