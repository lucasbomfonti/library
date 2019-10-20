using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Business.Service.Contracts
{
    public interface IUserService : IBaseService<User, BaseFilter>
    {
    }
}