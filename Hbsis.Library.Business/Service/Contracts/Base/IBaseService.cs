using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hbsis.Library.CrossCutting.Filter.Base;

namespace Hbsis.Library.Business.Service.Contracts
{
    public interface IBaseService<T, TF> where T : class where TF : BaseFilter
    {
        Task<Guid> Create(T dto);

        Task<Guid> Create(T dto, DataContext context);

        Task<T> Update(T dto);

        Task<T> Update(T dto, DataContext context);

        Task Remove(Guid id);

        Task Remove(Guid id, DataContext context);

        Task<T> Find(Guid id);

        Task<T> Find(Guid id, DataContext context);

        Task<List<T>> All();

        Task<ResponseDto<T>> Search(RequestViewModel<TF> request);

        Task<ResponseDto<T>> Search(RequestViewModel<TF> request, DataContext context);

        DataContext GetContext();
    }
}