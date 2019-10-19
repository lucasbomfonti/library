using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts.Base;

namespace Hbsis.Library.Business.Service.Base
{
    public class BaseService<T, TT> : IBaseService<T, TT> where T : class where TT : BaseFilter
    {
        protected readonly IBaseRepository<T> BaseRepository;
        protected readonly IBaseRepositoryReadOnly<T, TT> BaseRepositoryReadOnly;

        public BaseService(IBaseRepositoryReadOnly<T, TT> baseRepositoryReadOnly, IBaseRepository<T> baseRepository)
        {
            BaseRepositoryReadOnly = baseRepositoryReadOnly;
            BaseRepository = baseRepository;
        }

        public async Task<Guid> Create(T dto)
        {
            return await BaseRepository.Create(dto);
        }

        public async Task<Guid> Create(T dto, DataContext context)
        {
            return await BaseRepository.Create(dto, context);
        }

        public async Task<T> Update(T dto)
        {
            return await BaseRepository.Update(dto);
        }

        public async Task<T> Update(T dto, DataContext context)
        {
            return await BaseRepository.Update(dto, context);
        }

        public async Task Remove(Guid id)
        {
            await BaseRepository.Remove(id);
        }

        public async Task Remove(Guid id, DataContext context)
        {
            await BaseRepository.Remove(id, context);
        }

        public async Task<T> Find(Guid id)
        {
            return await BaseRepositoryReadOnly.Find(id);
        }

        public async Task<T> Find(Guid id, DataContext context)
        {
            return await BaseRepositoryReadOnly.Find(id, context);
        }

        public async Task<List<T>> All()
        {
            return await BaseRepositoryReadOnly.All();
        }

        public async Task<ResponseDto<T>> Search(RequestViewModel<TT> request)
        {
            return await BaseRepositoryReadOnly.Search(request);
        }

        public async Task<ResponseDto<T>> Search(RequestViewModel<TT> request, DataContext context)
        {
            return await BaseRepositoryReadOnly.Search(request, context);
        }

        public DataContext GetContext()
        {
            return BaseRepository.GetContext();
        }
    }
}