using Hbsis.Library.CrossCutting.Interop.Base;

namespace Hbsis.Library.CrossCutting.Interop.ViewModel.Book
{
    public class BookUpdateViewModel : BaseUpdateViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}