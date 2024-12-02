using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApplication.ViewModels
{
    public class GenreViewModel : IMapFrom<Genre>, IMapTo<Genre>
    {
        [Required]
        public int GenreId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
