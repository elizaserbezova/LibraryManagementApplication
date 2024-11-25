using LibraryManagementApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Data.Repository.Interfaces
{
    public interface IBookRepository : IRepository<Book, int>
    {
        IEnumerable<Book> GetBooksByAuthor(int authorId);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
    }
}
