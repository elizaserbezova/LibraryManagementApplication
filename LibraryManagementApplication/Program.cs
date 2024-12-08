using LibraryManagementApplication.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagementApplication.Services.Mapping;

using LibraryManagementApplication.ViewModels;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Data.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.Services.Data;
using Microsoft.AspNetCore.Identity;

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
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IRepository<Book, int>, Repository<Book, int>>();
            builder.Services.AddScoped<IRepository<Author, int>, Repository<Author, int>>();
            builder.Services.AddScoped<IRepository<Member, int>, Repository<Member, int>>();
            builder.Services.AddScoped<IRepository<Genre, int>, Repository<Genre, int>>();
            builder.Services.AddScoped<IRepository<LendingRecord, int>, Repository<LendingRecord, int>>();

            // BOOKS
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();

            // AUTHORS
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();

            // GENRES
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IGenreService, GenreService>();

            //MEMBERS
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();

            // LENDING RECORD
            builder.Services.AddScoped<ILendingRecordRepository,  LendingRecordRepository>();
            builder.Services.AddScoped<ILendingService, LendingService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

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

            // HANDLE ERRORS
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                var roles = new[] { "Member", "Administrator" };

                foreach (var role in roles)
                {
                    bool roleExists = roleManager
                        .RoleExistsAsync(role)
                        .GetAwaiter()
                        .GetResult();

                    if (!roleExists)
                    {
                        var roleResult = roleManager
                            .CreateAsync(new IdentityRole(role))
                            .GetAwaiter()
                            .GetResult();
                    }
                }

                // SEED ADMIN
                string adminEmail = "admin@abv.bg";
                string adminPassword = "Admin1!";

                var adminUser = userManager
                    .FindByEmailAsync(adminEmail)
                    .GetAwaiter()
                    .GetResult();

                if (adminUser == null)
                {
                    adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                    var createUserResult = userManager
                        .CreateAsync(adminUser, adminPassword)
                        .GetAwaiter()
                        .GetResult();

                    if (createUserResult.Succeeded)
                    {
                        var addToRoleResult = userManager
                            .AddToRoleAsync(adminUser, "Administrator")
                            .GetAwaiter()
                            .GetResult();
                    }
                }
            }

            app.Run();
        }
    }
}
