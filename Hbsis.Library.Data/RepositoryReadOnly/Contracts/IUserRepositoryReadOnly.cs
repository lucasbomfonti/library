using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts.Base;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Data.RepositoryReadOnly.Contracts
{
    public interface IUserRepositoryReadOnly : IBaseRepositoryReadOnly<User, BaseFilter>
    {
        User Login(string name, string password);
    }
}