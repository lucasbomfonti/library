using System;
using System.Threading.Tasks;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hbsis.Library.Application.Contracts.Base
{
    public interface IBaseApplication<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> where T : class
                                                                    where TF : BaseFilter
                                                                    where TListDto : class
                                                                    where TDto : class
                                                                    where TInsertViewModel : class
                                                                    where TUpdateViewModel : BaseUpdateViewModel
    {
        Task<ActionResult> Create(TInsertViewModel dto);

        Task<ActionResult> Update(TUpdateViewModel dto);

        Task<ActionResult> Remove(Guid id);

        Task<ActionResult> Find(Guid id);

        Task<ActionResult> Search(int? page = null, int? perPage = null, object filter = null);
    }
}