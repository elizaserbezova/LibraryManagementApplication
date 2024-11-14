using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data.Models
{
    public class Author
    {
        [Key]
        [Comment("The id of the author")]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        [Comment("The name of the author")]
        public string Name { get; set; } = null!;

        [Comment("Biography of the author")]
        public string? Biography { get; set; }

        [Comment("Date of birth of the author")]
        public DateTime? DateOfBirth { get; set; }
    }
}
