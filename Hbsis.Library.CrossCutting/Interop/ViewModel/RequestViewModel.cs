using Hbsis.Library.CrossCutting.Filter.Base;

namespace Hbsis.Library.CrossCutting.Interop.ViewModel
{
    public class RequestViewModel<T> where T : BaseFilter
    {
        public RequestViewModel(T filter)
        {
            Filter = filter;
        }

        public RequestViewModel(int? page, int? perPage)
        {
            Page = page.HasValue && page.Value <= 0 ? 1 : page ?? 1;
            PerPage = perPage.HasValue && perPage.Value > 0 ? perPage.Value : 15;
        }

        public RequestViewModel(int? page, int? perPage, T filter)
        {
            Page = page.HasValue && page.Value <= 0 ? 1 : page ?? 1;
            PerPage = perPage.HasValue && perPage.Value > 0 ? perPage.Value : 15;
            Filter = filter;
        }

        public T Filter { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}