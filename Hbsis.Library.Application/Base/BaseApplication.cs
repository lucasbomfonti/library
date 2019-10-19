using Hbsis.Library.Application.Contracts.Base;
using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.CrossCutting.Helper;
using Hbsis.Library.CrossCutting.Interop.Base;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts.Base;
using Hbsis.Library.Domain.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Hbsis.Library.CrossCutting.Filter.Base;

namespace Hbsis.Library.Application.Base
{
    public class BaseApplication<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> :
        IBaseApplication<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> where T : BaseEntity
                                                                                    where TF : BaseFilter
                                                                                    where TDto : class
                                                                                    where TListDto : class
                                                                                    where TInsertViewModel : class
                                                                                    where TUpdateViewModel : BaseUpdateViewModel
    {
        protected readonly IBaseService<T, TF> Service;
        protected readonly IBaseRepositoryReadOnly<T, TF> BaseRepositoryReadOnly;

        public BaseApplication(IBaseService<T, TF> service, IBaseRepositoryReadOnly<T, TF> baseRepositoryReadOnly)
        {
            Service = service;
            BaseRepositoryReadOnly = baseRepositoryReadOnly;
        }
            
        public async Task<ActionResult> Create(TInsertViewModel dto)
        {
            var response = await Service.Create(MapperHelper.Map<TInsertViewModel, T>(dto));
            return await Task.FromResult(new ObjectResult(response) { StatusCode = (int?)HttpStatusCode.OK });
        }

        public async Task<ActionResult> Find(Guid id) => await Response(await BaseRepositoryReadOnly.Find(id), HttpStatusCode.OK);

        public async Task<ActionResult> Remove(Guid id) => await Response(Service.Remove(id), HttpStatusCode.NoContent);

        public async Task<ActionResult> Search(int? page = null, int? perPage = null, object filter = null)
        {
            if (page.HasValue || perPage.HasValue)
            {
                var response = await Service.Search(new RequestViewModel<TF>(page, perPage, (TF)filter));
                return await Response((MapperHelper.Map<ResponseDto<T>, ResponseDto<TListDto>>(response)), HttpStatusCode.OK);
            }

            return await Response(MapperHelper.CopyList<T, TListDto>(await Service.All()), HttpStatusCode.OK);
        }

        public async Task<ActionResult> Update(TUpdateViewModel dto)
        {
            var obj = await Service.Update(MapperHelper.Map<TUpdateViewModel, T>(dto));
            return await Response(MapperHelper.Map<T, TListDto>(obj), HttpStatusCode.OK);
        }

      

        protected virtual async Task<ActionResult> Response(object data, HttpStatusCode code) => await Task.FromResult(new ObjectResult(data) { StatusCode = (int)code });
    }
}