using Hbsis.Library.Api.Controllers;
using Hbsis.Library.Api.Test.Helper;
using Hbsis.Library.Application;
using Hbsis.Library.Application.Mapper;
using Hbsis.Library.Business.Service;
using Hbsis.Library.CrossCutting;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.Dto.User;
using Hbsis.Library.CrossCutting.Interop.ViewModel.User;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository;
using Hbsis.Library.Data.RepositoryReadOnly;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hbsis.Library.Api.Test
{
    public class UserControllerTest
    {
        private readonly UserController _UserController;

        public UserControllerTest()
        {
            MapperConfig.RegisterMappings();
            EnvironmentProperties.ConnectionString = string.Empty;
            TestHelper.PrepareDatabase<DataContext>();
            var context = new DataContext();
            var repositoryReadOnly = new UserRepositoryReadOnly(context);
            var repository = new UserRepository(context);
            var service = new UserService(repositoryReadOnly, repository);
            var application = new UserApplication(service, repositoryReadOnly);
            _UserController = new UserController(application);
        }

        [Fact]
        public async Task When_Called_Post_Should_Be_Ok()
        {
            var model = new UserInsertViewModel
            {
                Username = "User1",
                Password = "Password1",
                CheckPassword = "Password1"
            };

            var result = await _UserController.Post(model);
            var response = (ObjectResult)result;

            Assert.True(response.Value is Guid);
            Assert.True(response.StatusCode.Equals(200));
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_Without_Password()
        {
            var model = new UserInsertViewModel
            {
                Username = "User1",
                Password = null,
                CheckPassword = "Password1"
            };

            _UserController.ModelState.AddModelError("Password", "required");
            var result = await _UserController.Post(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Password"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_Without_Parameters()
        {
            await When_Called_Post_Should_Be_Ok();

            var result = await _UserController.Get();

            var okResult = (ObjectResult)result;
            Assert.True(okResult.Value is List<UserDto>);

            var resultModel = (List<UserDto>)okResult.Value;
            Assert.True(resultModel.Count > 0);
        }
    }
}