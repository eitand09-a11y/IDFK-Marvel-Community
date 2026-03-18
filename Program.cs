using Microsoft.EntityFrameworkCore;
using IDFK.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 1. ????? ????? ?-DbContext ?????? (Container)
// ????? ??? ????? ??? ???? ??? ??????? ?????? ?-appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // ??? ????? ????? ??? ?????
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddRazorPages();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ???? ?? ????? ??? ???:
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<IDFK.Hubs.ChatHub>("/chatHub");

app.Run();
