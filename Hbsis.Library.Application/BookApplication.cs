using Hbsis.Library.Application.Base;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application
{
    public class BookApplication : BaseApplication<Book, BookFilter, BookDto, BookDto, BookInsertViewModel, BookUpdateViewModel>, IBookApplication
    {
        public BookApplication(IBookService service, IBookRepositoryReadOnly baseRepositoryReadOnly) : base(service, baseRepositoryReadOnly)
        {
        }
    }
}