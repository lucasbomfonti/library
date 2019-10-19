using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository.Base;
using Hbsis.Library.Data.Repository.Contracts;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Data.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {
        }
    }
}