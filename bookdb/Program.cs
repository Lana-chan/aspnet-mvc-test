using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bookdb.Models;
using bookdb.Data;
using Microsoft.AspNetCore.Identity;
using ASPNetCoreIdentityCustomFields.Data;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<bookdbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("bookdbContext") ?? throw new InvalidOperationException("Connection string 'bookdbContext' not found.")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<bookdbContext>();

// Add DB context
builder.Services.AddDbContext<bookdbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("bookdbContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, builder.Configuration["UploadedFilesBase"])),
    RequestPath = "/" + builder.Configuration["UploadedFilesBase"]
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
