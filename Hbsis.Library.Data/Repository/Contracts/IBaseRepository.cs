using Hbsis.Library.Data.Context;
using System;
using System.Threading.Tasks;

namespace Hbsis.Library.Data.Repository.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<Guid> Create(T dto);

        Task<Guid> Create(T dto, DataContext context);

        Task<T> Update(T dto);

        Task<T> Update(T dto, DataContext context);

        Task Remove(Guid id);

        Task Remove(Guid id, DataContext context);

        DataContext GetContext();
    }
}