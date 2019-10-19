using Hbsis.Library.CrossCutting.Exceptions;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts.Base;
using Hbsis.Library.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hbsis.Library.CrossCutting.Filter.Base;

namespace Hbsis.Library.Data.RepositoryReadOnly.Base
{
    public class BaseRepositoryReadOnly<T, TT> : IBaseRepositoryReadOnly<T, TT> where T : BaseEntity where TT : BaseFilter
    {
        protected readonly DataContext Context;

        public BaseRepositoryReadOnly(DataContext context)
        {
            Context = context;
        }

        public async Task<T> Find(Guid id)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(f => f.Id.Equals(id) && f.Active) ?? throw new NotFoundException();
        }

        public virtual async Task<T> Find(Guid id, DataContext context)
        {
            return await context.Set<T>().FirstOrDefaultAsync(f => f.Id.Equals(id) && f.Active) ?? throw new NotFoundException();
        }

        public virtual async Task<List<T>> All()
        {
            return await Task.FromResult(Context.Set<T>().Where(w => w.Active).ToList());
        }

        public virtual async Task<ResponseDto<T>> Search(RequestViewModel<TT> request)
        {
            var query = Context.Set<T>().Where(w => w.Active).AsQueryable();
            query = Filter(request, query, Context);

            var total = query.Select(s => 1).Count();
            var skip = request.Page > 1 ? (request.Page - 1) * request.PerPage : 0;

            if (total <= request.PerPage)
            {
                skip = 0;
                request.Page = 1;
            }

            var temp = query.Skip(skip).Take(request.PerPage);

            return await Task.FromResult(new ResponseDto<T>
            {
                CurrentPage = request.Page,
                Data = ExtractFromContext(temp.ToList()),
                PerPage = request.PerPage,
                Total = total
            });
        }

        protected virtual IQueryable<T> Filter(RequestViewModel<TT> request, IQueryable<T> query, DataContext context)
        {
            return query;
        }

        public virtual async Task<ResponseDto<T>> Search(RequestViewModel<TT> request, DataContext context)
        {
            var query = context.Set<T>().Where(w => w.Active).AsQueryable();
            query = Filter(request, query, context);

            var total = query.Select(s => 1).Count();
            var skip = request.Page > 1 ? (request.Page - 1) * request.PerPage : 0;

            if (total <= request.PerPage)
            {
                skip = 0;
                request.Page = 1;
            }

            var temp = query.Skip(skip).Take(request.PerPage);

            return await Task.FromResult(new ResponseDto<T>
            {
                CurrentPage = request.Page,
                Data = ExtractFromContext(temp.ToList()),
                PerPage = request.PerPage,
                Total = total
            });
        }

        protected TE ExtractFromContext<TE>(TE dto)
        {
            var temp = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<TE>(temp);
        }

        protected List<TE> ExtractFromContext<TE>(List<TE> dto)
        {
            var temp = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<List<TE>>(temp);
        }

        protected List<TE> ExtractFromContext<TE>(IEnumerable<TE> dto)
        {
            return ExtractFromContext(dto.ToList());
        }

        public DataContext GetContext() => new DataContext();
    }
}