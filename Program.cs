using BanSachCu.Controllers;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Sach.Model.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapAreaControllerRoute(
    name: "Administrator",
    areaName: "Administrator",
    pattern: "Administrator/{controller=Home}/{action=Index}/{id?}");
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

