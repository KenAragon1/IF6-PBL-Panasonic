using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using panasonic.Helpers;
using panasonic.Repositories;

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

// user
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IAreaMaterialRepository, AreaMaterialRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IFileHelper, FileHelper>();

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

// Tambahkan routing untuk MaterialRequest
app.MapControllerRoute(
    name: "materialrequest",
    pattern: "MaterialRequest/{action=Index}/{id?}",
    defaults: new { controller = "MaterialRequest" }
);

//Route preproom
app.MapControllerRoute(
    name: "preproom",
    pattern: "PrepRoom/{action=Index}/{id?}",
    defaults: new { controller = "PrepRoom" }
);

//Route for Material Usage
app.MapControllerRoute(
    name: "materialusage",
    pattern: "MaterialUsage/{action=Index}/{id?}",
    defaults: new { controller = "MaterialUsage" }
);

//Route material return
app.MapControllerRoute(
    name: "materialreturn",
    pattern: "MaterialReturn/{action=Index}/{id?}",
    defaults: new { controller = "MaterialReturn" }
);

app.MapControllerRoute(
    name: "managematerialrequest",
    pattern: "managematerialrequest/{action=Index}/{id?}",
    defaults: new { controller = "managematerialrequest" }
);

app.MapControllerRoute(
    name: "sendmaterialreturn",
    pattern: "sendmaterialreturn/{action=Index}/{id?}",
    defaults: new { controller = "sendmaterialreturn" }
);

app.Run();
