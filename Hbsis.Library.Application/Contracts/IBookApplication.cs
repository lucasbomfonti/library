using Hbsis.Library.Application.Contracts.Base;
using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application.Contracts
{
    public interface IBookApplication : IBaseApplication<Book, BookFilter, BookDto, BookDto, BookInsertViewModel, BookUpdateViewModel>
    {
    }
}