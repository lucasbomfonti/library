using Hbsis.Library.Domain.Base;

namespace Hbsis.Library.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}