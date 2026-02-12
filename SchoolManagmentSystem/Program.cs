using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Infrastructure.Repositories.UnitOfWork;
using SchoolManagement.Infrastructure.Services;
using SchoolManagment.Application.Interface.IUnitOfWork;
using SchoolManagment.Application.Interface.Services;
using SchoolManagment.Application.Interface.StudentRepository;
using SchoolManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")
        ?? "Server=.;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;"));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.MapStaticAssets();
//app.MapRazorPages()
//   .WithStaticAssets();

//await SeedData.InitializeAsync(app.Services);

app.Run();
