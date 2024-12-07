using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApplication.ViewModels
{
    public class LentBookViewModel
    {
        public int LendingRecordId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
    }
}
