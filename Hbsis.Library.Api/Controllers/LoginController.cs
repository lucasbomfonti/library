using Hbsis.Library.Api.Controllers.Base;
using Hbsis.Library.Api.Security;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.CrossCutting.Extensions;
using Hbsis.Library.CrossCutting.Filter.Base;
using Hbsis.Library.CrossCutting.Interop.Dto.User;
using Hbsis.Library.CrossCutting.Interop.ViewModel.User;
using Hbsis.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hbsis.Library.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LoginController : BaseController<User, BaseFilter, UserDto, UserDto, UserInsertViewModel, UserUpdateViewModel>
    {
        public LoginController(IUserApplication application) : base(application)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());
            var userApp = Application as IUserApplication;
            return Ok(UserManagement.RegisterUser(userApp?.Login(model.Name, model.Password)));
        }
    }
}