using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Login with a cookie: after logging in, the browser keeps a cookie that proves who you are.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";          // where to send people who are not logged in
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8); // log out after 8 hours of no activity
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;                 // JavaScript cannot read the login cookie
    });

// Turns passwords into one-way hashes, and checks them at login.
builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Every POST (create, edit, delete) must carry a valid security token.
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

    // Every page needs a logged-in admin, unless it is marked [AllowAnonymous].
    var loggedInAdminsOnly = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(loggedInAdminsOnly));
});

var app = builder.Build();

// First run on a new computer: create the database and its tables.
// If the database already has tables, this does nothing (your data is safe).
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
    dbContext.Database.EnsureCreated();

    // Databases made before the login feature have no Admins table: add it once.
    dbContext.Database.ExecuteSqlRaw(@"
        IF OBJECT_ID(N'dbo.Admins', N'U') IS NULL
        BEGIN
            CREATE TABLE dbo.Admins (
                AdminId      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Admins PRIMARY KEY,
                Username     NVARCHAR(50)      NOT NULL,
                PasswordHash NVARCHAR(MAX)     NOT NULL
            );
            CREATE UNIQUE INDEX IX_Admins_Username ON dbo.Admins (Username);
        END");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();   // who are you? (reads the login cookie)
app.UseAuthorization();    // are you allowed here?

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
