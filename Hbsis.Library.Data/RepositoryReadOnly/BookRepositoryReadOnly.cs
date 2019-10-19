using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.RepositoryReadOnly.Base;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hbsis.Library.Data.RepositoryReadOnly
{
    public class BookRepositoryReadOnly : BaseRepositoryReadOnly<Book, BookFilter>, IBookRepositoryReadOnly
    {
        public BookRepositoryReadOnly(DataContext context) : base(context)
        {
        }

        protected override IQueryable<Book> Filter(RequestViewModel<BookFilter> request, IQueryable<Book> query, DataContext context)
        {
            if (!string.IsNullOrEmpty(request.Filter.Term))
            {
                var term = request.Filter.Term;
                query = query.Where(w => w.Author.ToUpper().Contains(term) || w.Description.ToUpper().Contains(term) || w.Title.ToUpper().Contains(term));
            }

            switch (request.Filter.Ordination)
            {
                case BookOrdination.Title:
                    query = request.Filter.Ascending ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                    break;

                case BookOrdination.Author:
                    query = request.Filter.Ascending ? query.OrderBy(o => o.Author) : query.OrderByDescending(o => o.Author);
                    break;

                case BookOrdination.Description:
                    query = request.Filter.Ascending ? query.OrderBy(o => o.Description) : query.OrderByDescending(o => o.Description);
                    break;
            }

            return base.Filter(request, query, context);
        }

        public override async Task<List<Book>> All() => await Task.FromResult(Context.Set<Book>().Where(w => w.Active).OrderBy(a => a.Title).ToList());
    }
}