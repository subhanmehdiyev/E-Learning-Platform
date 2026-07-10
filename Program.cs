using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_Learning_Platform.Data;
using Microsoft.AspNetCore.Identity;
using E_Learning_Platform.Models;
using E_Learning_Platform.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages( options => 
{
    options.Conventions.AuthorizeFolder("/Courses", "InstructorOnly");
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/CourseResultPages", "InstructorOnly");
});

builder.Services.AddDbContext<E_Learning_PlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("E_Learning_PlatformContext") ?? throw new InvalidOperationException("Connection string 'E_Learning_PlatformContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<E_Learning_PlatformContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("InstructorOnly", policy => policy.RequireClaim("Position", "Instructor"));
    options.AddPolicy("StudentOnly", policy => policy.RequireClaim("Position", "Student"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
