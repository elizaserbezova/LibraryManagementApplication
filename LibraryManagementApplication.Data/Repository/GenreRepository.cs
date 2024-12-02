using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;

namespace LibraryManagementApplication.Data.Repository
{
    public class GenreRepository : Repository<Genre, int>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
        }
    }
}
