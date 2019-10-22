using System.ComponentModel.DataAnnotations;

namespace Hbsis.Library.CrossCutting.Interop.ViewModel.User
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
