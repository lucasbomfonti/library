using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository.Base;
using Hbsis.Library.Data.Repository.Contracts;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}