using Hbsis.Library.Application.Contracts.Base;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Helper;
using Hbsis.Library.CrossCutting.Interop.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hbsis.Library.Api.Controllers.Base
{

    public class BaseController<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> : ControllerBase where T : class
                                                                                                            where TF : BaseFilter
                                                                                                            where TListDto : class
                                                                                                            where TDto : class
                                                                                                            where TInsertViewModel : class
                                                                                                            where TUpdateViewModel : BaseUpdateViewModel
    {
        protected readonly IBaseApplication<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> Application;

        public BaseController(IBaseApplication<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> application)
        {
            Application = application;
        }

        protected async Task<ActionResult> Post(TInsertViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            return await Application.Create(viewModel);
        }

        protected async Task<ActionResult> Update(TUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            return await Application.Update(viewModel);
        }

        protected async Task<ActionResult> Delete(Guid id) => await Application.Remove(id);

        protected async Task<ActionResult> Find(Guid id) => await Application.Find(id);

        protected async Task<ActionResult> Get(int? page = null, int? perPage = null, object filter = null) => await Application.Search(page, perPage, filter);
    }
}