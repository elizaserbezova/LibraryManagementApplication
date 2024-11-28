using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;

namespace LibraryManagementApplication.Data.Repository
{
    public class AuthorRepository : Repository<Author, int>, IAuthorRepository 
    {
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
