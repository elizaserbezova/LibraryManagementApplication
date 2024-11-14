using LibraryManagementApplication.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Genre>()
                .HasData(
                    new Genre { GenreId = 1, Name = "Biography" },
                    new Genre { GenreId = 2, Name = "Horror" },
                    new Genre { GenreId = 3, Name = "Crime" },
                    new Genre { GenreId = 4, Name = "History" },
                    new Genre { GenreId = 5, Name = "Romance" },
                    new Genre { GenreId = 6, Name = "Classic" });
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<LendingRecord> LendingRecords { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
