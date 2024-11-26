using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApplication.ViewModels
{
    public class BookViewModel : IMapFrom<Book>
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string ISBN { get; set; } = null!;
        public DateTime PublishDate { get; set; }

        public string AuthorName { get; set; } = null!;

        public string GenreName { get; set; } = null!;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public List<Author> Authors { get; set; } = new List<Author>();

        [Required]
        public int GenreId { get; set; }

        [Required]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public bool AvailabilityStatus { get; set; }
    }
}
