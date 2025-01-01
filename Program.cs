using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using panasonic.Helpers;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Auth/Login";
    option.AccessDeniedPath = "/Auth/Login";
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

// Helper
builder.Services.AddScoped<IUserClaimHelper, UserClaimHelper>();
builder.Services.AddScoped<IFileHelper, FileHelper>();


builder.Services.AddScoped<IAuthService, AuthService>();

// user
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();

builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();

builder.Services.AddScoped<IMaterialRequestService, MaterialRequestService>();
builder.Services.AddScoped<IMaterialRequestRepository, MaterialRequestRepository>();


builder.Services.AddScoped<IMaterialInventoryService, MaterialInventoryService>();
builder.Services.AddScoped<IMaterialInventoryRepository, MaterialInventoryRepository>();

builder.Services.AddScoped<IMaterialTransactionRepository, MaterialTransactionRepository>();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");



app.Run();
