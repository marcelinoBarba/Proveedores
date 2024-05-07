
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using WebProveedores.Models.Data;
using static System.Net.WebRequestMethods;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var path = Directory.GetCurrentDirectory();

Log.Logger = new LoggerConfiguration()
        .WriteTo.File($"{path}\\Logs\\{DateTime.Now.ToString("yyyyMMdd")}\\Log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
        .CreateLogger();

builder.Services.AddLogging(builder =>
{
    var path = Directory.GetCurrentDirectory();
    builder.AddSerilog();
    builder.AddFilter("Microsoft", LogLevel.Warning)
           .AddFilter("System", LogLevel.Warning);

});



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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
