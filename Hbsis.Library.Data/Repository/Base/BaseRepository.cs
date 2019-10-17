using Hbsis.Library.CrossCutting.Exceptions;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository.Contracts;
using Hbsis.Library.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hbsis.Library.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context;

        public BaseRepository(DataContext context)
        {
            Context = context;
        }

        public async Task<Guid> Create(T dto)
        {
            await Context.Set<T>().AddAsync(dto);
            await Context.SaveChangesAsync();
            return dto.Id;
        }

        public async Task<Guid> Create(T dto, DataContext context)
        {
            await context.Set<T>().AddAsync(dto);
            await context.SaveChangesAsync();
            return dto.Id;
        }

        public async Task<T> Update(T dto)
        {
            var currentValue = SetValue(dto, Context);
            await Context.SaveChangesAsync();
            return ExtractFromContext(currentValue);
        }

        public async Task<T> Update(T dto, DataContext context)
        {
            var currentValue = SetValue(dto, context);
            await context.SaveChangesAsync();
            return ExtractFromContext(currentValue);
        }

        public async Task Remove(Guid id)
        {
            var obj = await Context.Set<T>().FirstAsync(f => f.Id.Equals(id));
            obj.Active = false;
            SetValue(obj, Context);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Guid id, DataContext context)
        {
            var obj = await context.Set<T>().FirstAsync(f => f.Id.Equals(id));
            obj.Active = false;
            SetValue(obj, context);
            await Context.SaveChangesAsync();
        }

        public DataContext GetContext()
        {
            return new DataContext();
        }

        private T SetValue(T obj, DataContext context)
        {
            var currentValue = context.Set<T>().Find(obj.Id);
            if (currentValue?.Version != obj.Version)
                throw new VersionException("Record outdated, update and try again");

            SetValue(context, obj, currentValue);
            currentValue.Date = DateTime.Now;
            return currentValue;
        }

        protected virtual void SetValue(DataContext context, T obj, T currentValue)
        {
            obj.Version++;
            context.Entry(currentValue).CurrentValues.SetValues(obj);
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
    }
}