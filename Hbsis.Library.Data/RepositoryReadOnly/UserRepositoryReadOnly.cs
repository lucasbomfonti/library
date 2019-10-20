using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Helper;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.RepositoryReadOnly.Base;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain;
using System.Linq;
using Hbsis.Library.CrossCutting.Exceptions;

namespace Hbsis.Library.Data.RepositoryReadOnly
{
    public class UserRepositoryReadOnly : BaseRepositoryReadOnly<User, BaseFilter>, IUserRepositoryReadOnly
    {
        public UserRepositoryReadOnly(DataContext context) : base(context)
        {
        }

        public User Login(string name, string password)
        {
            password = EncryptHelper.EncryptPassword(name, password);
            var response = Context.Set<User>().FirstOrDefault(w => w.Active && w.Username.Equals(name) && w.Password.Equals(password)) ?? throw new NotFoundException("username or password is invalid");
            return ExtractFromContext(response);
        }
    }
}