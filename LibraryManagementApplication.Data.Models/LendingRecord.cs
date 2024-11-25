using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace LibraryManagementApplication.Data.Models
{
    public class LendingRecord
    {
        [Key]
        [Comment("The id of the lending record")]
        public int LendingRecordId { get; set; }

        [ForeignKey(nameof(Book))]
        [Comment("The id of the borrowed book")]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [ForeignKey(nameof(Member))]
        [Comment("The id of the member who borrowed the book")]
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        [Comment("The date when the book was borrowed")]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Comment("The date when the book was returned")]
        public DateTime? ReturnDate { get; set; }
    }
}
