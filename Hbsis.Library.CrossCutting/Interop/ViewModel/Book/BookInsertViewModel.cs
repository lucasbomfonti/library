using System.ComponentModel.DataAnnotations;

namespace Hbsis.Library.CrossCutting.Interop.ViewModel.Book
{
    public class BookInsertViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}