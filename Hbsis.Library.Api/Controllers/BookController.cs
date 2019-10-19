using Hbsis.Library.Api.Controllers.Base;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hbsis.Library.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class BookController : BaseController<Book, BookFilter, BookDto, BookDto, BookInsertViewModel, BookUpdateViewModel>
    {
        public BookController(IBookApplication application) : base(application)
        {
        }

        [HttpGet("{id}")]
        public new async Task<ActionResult<BookDto>> Find(Guid id) => await base.Find(id);

        [HttpGet]
        public async Task<ActionResult<BookDto>> Get(int? page = null, int? perPage = null, string term = null, bool? ascending = null, BookOrdination? ordination = null)
        {
            var filter = new BookFilter(term, ascending, ordination);
            return await Get(page, perPage, filter);
        }

        [HttpPost]
        public async new Task<ActionResult<BookDto>> Post([FromBody] BookInsertViewModel viewModel) => await base.Post(viewModel);

        [HttpPut]
        public async Task<ActionResult<BookDto>> Put([FromBody] BookUpdateViewModel viewModel) => await Update(viewModel);

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDto>> Remove(Guid id) => await Delete(id);
    }
}