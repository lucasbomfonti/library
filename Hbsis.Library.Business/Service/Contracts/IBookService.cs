using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Business.Service.Contracts
{
    public interface IBookService : IBaseService<Book, BookFilter>
    {
    }
}