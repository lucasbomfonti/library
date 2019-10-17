using Hbsis.Library.CrossCutting.Interop.Base;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Hbsis.Library.Domain.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> Find(Guid id, DataContext context)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> All()
        {
            return await Task.FromResult(Context.Set<T>().Where(w => w.Active).ToList());
        }

        public async Task<ResponseDto<T>> Search(RequestViewModel<TT> request)
        {
            var query = Context.Set<T>().AsQueryable();
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

        public async Task<ResponseDto<T>> Search(RequestViewModel<TT> request, DataContext context)
        {
            throw new NotImplementedException();
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

        public DataContext GetContext()
        {
            throw new NotImplementedException();
        }
    }
}