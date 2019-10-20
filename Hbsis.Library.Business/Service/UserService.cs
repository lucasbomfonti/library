using Hbsis.Library.Business.Service.Base;
using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Helper;
using Hbsis.Library.Data.Repository.Contracts;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain;
using System;
using System.Threading.Tasks;

namespace Hbsis.Library.Business.Service
{
    public class UserService : BaseService<User, BaseFilter>, IUserService
    {
        public UserService(IUserRepositoryReadOnly baseRepositoryReadOnly, IUserRepository baseRepository) : base(baseRepositoryReadOnly, baseRepository)
        {
        }

        public override Task<Guid> Create(User dto)
        {
            dto.Password = EncryptHelper.EncryptPassword(dto.Username, dto.Password);
            return base.Create(dto);
        }
    }
}