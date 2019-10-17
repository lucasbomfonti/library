using System;
using System.Collections.Generic;

namespace Hbsis.Library.CrossCutting.Interop.Dto
{
    public class ResponseDto<T> where T : class
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int LastPage => PerPage > 0 ? Convert.ToInt32(Math.Ceiling((double)Total / PerPage)) : 1;
        public int Total { get; set; }
    }
}