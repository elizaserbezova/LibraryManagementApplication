using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.ViewModels
{
    public class AuthorViewModel : IMapFrom<Author>, IMapTo<Author>
    {
        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Author Name must be at most 100 characters.")]
        public string Name { get; set; } = null!;

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
