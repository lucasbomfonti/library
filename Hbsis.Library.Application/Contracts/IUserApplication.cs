using Hbsis.Library.Application.Contracts.Base;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Dto.User;
using Hbsis.Library.CrossCutting.Interop.ViewModel.User;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application.Contracts
{
    public interface IUserApplication : IBaseApplication<User, BaseFilter, UserDto, UserDto, UserInsertViewModel, UserUpdateViewModel>
    {
        User Login(string login, string password);
    }
}