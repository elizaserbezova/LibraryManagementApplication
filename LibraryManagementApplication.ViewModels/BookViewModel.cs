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
        [StringLength(255, ErrorMessage = "The Title must be at most 255 characters.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(13, ErrorMessage = "The ISBN must be at most 13 characters.")]
        public string ISBN { get; set; } = null!;
        public DateTime PublishDate { get; set; }

        public string? AuthorName { get; set; }

        public string? GenreName { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public List<Author> Authors { get; set; } = new List<Author>();

        [Required]
        public int GenreId { get; set; }

        [Required]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public bool AvailabilityStatus { get; set; }

        public bool IsLentByUser { get; set; }

        public int? LendingRecordId { get; set; }
    }
}
