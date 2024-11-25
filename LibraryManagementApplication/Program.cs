using LibraryManagementApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryManagementApplication.Services.Mapping;
using LibraryManagementApplication.Services.D


using LibraryManagementApplication.ViewModels;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Data.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.Services.Data;

namespace LibraryManagementApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IRepository<Book, int>, Repository<Book, int>>();
            builder.Services.AddScoped<IRepository<Author, int>, Repository<Author, int>>();
            builder.Services.AddScoped<IRepository<Member, int>, Repository<Member, int>>();
            builder.Services.AddScoped<IRepository<Genre, int>, Repository<Genre, int>>();
            builder.Services.AddScoped<IRepository<LendingRecord, int>, Repository<LendingRecord, int>>();

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
            app.MapRazorPages();

            app.Run();
        }
    }
}
