using Hbsis.Library.CrossCutting.Interop.Base;

namespace Hbsis.Library.CrossCutting.Interop.ViewModel.User
{
    public class UserUpdateViewModel : BaseUpdateViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}