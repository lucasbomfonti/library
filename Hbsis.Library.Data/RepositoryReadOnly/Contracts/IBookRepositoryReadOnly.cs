using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts.Base;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Data.RepositoryReadOnly.Contracts
{
    public interface IBookRepositoryReadOnly : IBaseRepositoryReadOnly<Book, BookFilter>
    {
    }
}