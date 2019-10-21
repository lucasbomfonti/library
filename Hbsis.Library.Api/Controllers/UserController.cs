using System;
using System.Net;
using Hbsis.Library.Api.Controllers.Base;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Dto.User;
using Hbsis.Library.CrossCutting.Interop.ViewModel.User;
using Hbsis.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hbsis.Library.Api.Controllers
{
    [Route("api/v1/users")]
    public class UserController : BaseController<User, BaseFilter, UserDto, UserDto, UserInsertViewModel, UserUpdateViewModel>
    {
        public UserController(IUserApplication application) : base(application)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get() => await base.Get();

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async new Task<ActionResult> Post([FromBody] UserInsertViewModel viewModel) => await base.Post(viewModel);
    }
}