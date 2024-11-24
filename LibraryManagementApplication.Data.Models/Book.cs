using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data.Models
{
    public class Book
    {
        [Key]
        [Comment("The id of the book")]
        public int BookId { get; set; }

        [Required]
        [StringLength(255)]
        [Comment("The title of the book")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(13)]
        [Comment("The ISBN of the book")]
        public string ISBN { get; set; } = null!;

        [Comment("The publication date of the book")]
        public DateTime PublishDate { get; set; }

        [ForeignKey(nameof(Author))]
        [Comment("The id of the author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        [ForeignKey(nameof(Genre))]
        [Comment("The id of the genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        [Comment("Availability status of the book")]
        public bool AvailabilityStatus { get; set; } = true; // Default to available
    }
}
