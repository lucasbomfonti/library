﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.ViewModel;
using Hbsis.Library.Data.Context;

namespace Hbsis.Library.Data.RepositoryReadOnly.Contracts.Base
{
    public interface IBaseRepositoryReadOnly<T, TT> where T : class where TT : BaseFilter
    {
        Task<T> Find(Guid id);

        Task<T> Find(Guid id, DataContext context);

        Task<List<T>> All();

        Task<ResponseDto<T>> Search(RequestViewModel<TT> request);

        Task<ResponseDto<T>> Search(RequestViewModel<TT> request, DataContext context);

        DataContext GetContext();
    }
}