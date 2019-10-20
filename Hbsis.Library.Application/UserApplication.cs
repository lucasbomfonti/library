using Hbsis.Library.Application.Base;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Dto.User;
using Hbsis.Library.CrossCutting.Interop.ViewModel.User;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application
{
    public class UserApplication : BaseApplication<User, BaseFilter, UserDto, UserDto, UserInsertViewModel, UserUpdateViewModel>, IUserApplication
    {
        public UserApplication(IUserService service, IUserRepositoryReadOnly baseRepositoryReadOnly) : base(service, baseRepositoryReadOnly)
        {
        }

        public User Login(string login, string password)
        {
            return ((IUserRepositoryReadOnly)BaseRepositoryReadOnly).Login(login, password);
        }
    }
}