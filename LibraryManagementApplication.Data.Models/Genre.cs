using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data.Models
{
    public class Genre
    {
        [Key]
        [Comment("ID of the genre")]
        public int GenreId { get; set; }

        [Required]
        [StringLength(50)]
        [Comment("The name of the genre")]
        public string Name { get; set; } = null!;

        [Comment("A description of the genre")]
        public string? Description { get; set; }
    }
}
