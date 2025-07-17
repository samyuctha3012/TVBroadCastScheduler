using Microsoft.EntityFrameworkCore;
using System;
using TVBroadcast.BLL.Services;
using TVBroadcast.DAL.Context;
using TVBroadcast.DAL.Repositories;
using TVBroadcast.Domain.IRepository;
using TVBroadcast.Domain.IServices;

var builder = WebApplication.CreateBuilder(args);

// 💾 Connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🎀 Register Services and Repositories
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IShowService, ShowService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Schedule}/{action=Index}/{id?}");

app.Run();
