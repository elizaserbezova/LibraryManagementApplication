using LibraryManagementApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Data.Repository.Interfaces
{
    public interface IAuthorRepository : IRepository<Author, int>
    {
    }
}
