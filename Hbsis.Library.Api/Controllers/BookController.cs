using Hbsis.Library.Api.Controllers.Base;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hbsis.Library.Api.Security;

namespace Hbsis.Library.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class BookController : BaseController<Book, BookFilter, BookDto, BookDto, BookInsertViewModel, BookUpdateViewModel>
    {
        public BookController(IBookApplication application) : base(application)
        {
        }

        [HttpGet("{id}")]
        public new async Task<ActionResult> Find(Guid id) => await base.Find(id);

        [HttpGet]
        public async Task<ActionResult> Get(int? page = null, int? perPage = null, string term = null, bool? ascending = null, BookOrdination? ordination = null)
        {
            var filter = new BookFilter(term, ascending, ordination);
            return await Get(page, perPage, filter);
        }

        [HttpPost]
        [AccessValidation]
        public async new Task<ActionResult> Post([FromBody] BookInsertViewModel viewModel) => await base.Post(viewModel);

        [HttpPut]
        [AccessValidation]
        public async Task<ActionResult> Put([FromBody] BookUpdateViewModel viewModel) => await Update(viewModel);

        [AccessValidation]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(Guid id) => await Delete(id);
    }
}