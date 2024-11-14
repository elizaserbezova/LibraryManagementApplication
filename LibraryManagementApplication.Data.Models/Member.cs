using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data.Models
{
    public class Member
    {
        [Key]
        [Comment("The id of the member")]
        public int MemberId { get; set; }

        [Required]
        [StringLength(100)]
        [Comment("The name of the member")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(255)]
        [Comment("The email of the member")]
        public string Email { get; set; } = null!;

        [Comment("The date when the membership started")]
        public DateTime MembershipDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(15)]
        [Comment("The contact information of the member")]
        public string ContactInfo { get; set; } = null!;
    }
}
