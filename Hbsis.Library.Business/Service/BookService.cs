using Hbsis.Library.Business.Service.Base;
using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.Data.Repository.Contracts;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Business.Service
{
    public class BookService : BaseService<Book, BookFilter>, IBookService
    {
        public BookService(IBookRepositoryReadOnly baseRepositoryReadOnly, IBookRepository baseRepository) : base(baseRepositoryReadOnly, baseRepository)
        {
        }
    }
}