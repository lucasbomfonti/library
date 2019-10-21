using Hbsis.Library.Api.Controllers.Base;
using Hbsis.Library.Api.Security;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.CrossCutting.Filter;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Hbsis.Library.Api.Controllers
{
    [Route("api/v1/books")]
    public class BookController : BaseController<Book, BookFilter, BookDto, BookDto, BookInsertViewModel, BookUpdateViewModel>
    {
        public BookController(IBookApplication application) : base(application)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public new async Task<ActionResult> Find(Guid id) => await base.Find(id);

        [HttpGet]
        [ProducesResponseType(typeof(List<BookDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get(int? page = null, int? perPage = null, string term = null, bool? ascending = null, BookOrdination? ordination = null)
        {
            var filter = new BookFilter(term, ascending, ordination);
            return await Get(page, perPage, filter);
        }

        [HttpPost]
        [AccessValidation]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async new Task<ActionResult> Post([FromBody] BookInsertViewModel viewModel) => await base.Post(viewModel);

        [HttpPut]
        [AccessValidation]
        [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Put([FromBody] BookUpdateViewModel viewModel) => await Update(viewModel);

        [AccessValidation]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Remove(Guid id) => await Delete(id);
    }
}