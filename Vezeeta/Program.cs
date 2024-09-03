using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Helpers;
using Vezeeta.Service.Interface;
using Vezeeta.Service.Repositories;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ConnectionStrings
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Defult"))
    );

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

// Auto Mapper
builder.Services.AddAutoMapper(m => m.AddProfile(new AutoMapperProfile()));

#region Dependency Injection Settings

builder.Services.AddTransient<IContactUsRepository, ContactUsRepository>();

builder.Services.AddTransient<IPatientRepository, PatientRepository>();

builder.Services.AddTransient<IAreaRepository, AreaRepository>();

builder.Services.AddTransient<IFileRepository, FileRepository>();

#endregion

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
app.MapRazorPages();

app.Run();
