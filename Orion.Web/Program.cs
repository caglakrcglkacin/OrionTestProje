using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orion.Bussines.Diger;
using Orion.Bussines.Ýnterface;
using Orion.Bussines.Service;
using Orion.DataAccess;
using Orion.Domain;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = new PathString("/Home/Index");
//    options.LogoutPath = new PathString("/User/Logout");
//    options.AccessDeniedPath = new PathString("/Home/AccessDenied");

//    options.Cookie = new()
//    {
//        Name = "IdentityCookie",
//        HttpOnly = true,
//        SameSite = SameSiteMode.Lax,
//        SecurePolicy = CookieSecurePolicy.Always
//    };
//    options.SlidingExpiration = true;
//    options.ExpireTimeSpan = TimeSpan.FromDays(30);
//});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Bu çerez ilkesi, tanýmlanmamýþ çerezlerin yanýltýlmadan uygulanmasýný saðlar.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
var context = builder.Configuration.GetConnectionString("BinteConnetion");
builder.Services.AddControllersWithViews();
Trace.Listeners.Add(new TextWriterTraceListener("error-logs.txt"));//hatalarý yazdýrma
Trace.AutoFlush = true;
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();
builder.Services.AddDataProtection();
builder.Services.AddMvc();
builder.Services.AddDbContext<AppDbContext>(p =>
{
    p.UseSqlServer(context);
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUser, Users>();
builder.Services.AddScoped<IContact, Contacts>();
builder.Services.AddScoped<UserMangerSing>();
// Add services to the container.
builder.Services.AddControllersWithViews();
 builder.Services.AddIdentity<User,Roles>().AddUserManager<UserMangerSing>()
    .AddEntityFrameworkStores<AppDbContext>(); 
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
