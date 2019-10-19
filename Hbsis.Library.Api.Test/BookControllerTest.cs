using Hbsis.Library.Api.Controllers;
using Hbsis.Library.Api.Test.Helper;
using Hbsis.Library.Application;
using Hbsis.Library.Application.Mapper;
using Hbsis.Library.Business.Service;
using Hbsis.Library.CrossCutting;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository;
using Hbsis.Library.Data.RepositoryReadOnly;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hbsis.Library.Api.Test
{
    public class BookControllerTest
    {
        private readonly BookController _bookController;

        public BookControllerTest()
        {
            MapperConfig.RegisterMappings();
            EnvironmentProperties.ConnectionString = string.Empty;
            TestHelper.PrepareDatabase<DataContext>();
            var context = new DataContext();
            var repositoryReadOnly = new BookRepositoryReadOnly(context);
            var repository = new BookRepository(context);
            var service = new BookService(repositoryReadOnly, repository);
            var application = new BookApplication(service, repositoryReadOnly);
            _bookController = new BookController(application);
        }

        [Fact]
        public async Task Ok_WhenCalled_Post_Without_Cover()
        {
            var model = new BookInsertViewModel
            {
                Title = "Book 1",
                Description = "There is a book 1",
                Author = "Author 1",
                Image = null
            };

            var result = await _bookController.Post(model);
            Assert.True(result.Result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Ok_WhenCalled_Post_With_Cover()
        {
            var model = new BookInsertViewModel
            {
                Title = "Book 1",
                Description = "There is a book 1",
                Author = "Author 1",
                Image = "http://localhost/imagem.jpg"
            };

            var result = await _bookController.Post(model);
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task BadRequest_WhenCalled_Post_Without_Title()
        {
            var model = new BookInsertViewModel
            {
                Description = "There is a book 1",
                Author = "Author 1"
            };

            var result = await _bookController.Post(model);

            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Title"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task BadRequest_WhenCalled_Post_Without_Description()
        {
            var model = new BookInsertViewModel
            {
                Title = "Book 1",
                Author = "Author 1"
            };

            var result = await _bookController.Post(model);

            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Description"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task BadRequest_WhenCalled_Post_Without_Author()
        {
            var model = new BookInsertViewModel
            {
                Title = "Book 1",
                Description = "There is a book 1"
            };

            var result = await _bookController.Post(model);

            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Author"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_Without_Parameters()
        {
            Ok_WhenCalled_Post_Without_Cover();

            var result = await _bookController.Get();

            var okResult = (OkObjectResult)result.Result;
            Assert.True(okResult.Value is List<BookDto>);

            var resultModel = (List<BookDto>)okResult.Value;
            Assert.True(resultModel.Count > 0);
        }

        [Fact]
        public async Task<List<BookDto>> Ok_WhenCalled_Get_With_PageControl()
        {
            Ok_WhenCalled_Post_Without_Cover();

            var result = await _bookController.Get(1, 100);

            var okResult = (OkObjectResult)result.Result;
            Assert.True(okResult.Value is ResponseDto<BookDto>);

            var resultModel = (ResponseDto<BookDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            return resultModel.Data.ToList();
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_With_PageControl_Filter()
        {
            Ok_WhenCalled_Post_Without_Cover();

            var result = await _bookController.Get(1, 10, "Book 1");

            var okResult = (OkObjectResult)result.Result;
            Assert.True(okResult.Value is ResponseDto<BookDto>);

            var resultModel = (ResponseDto<BookDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
        }

        [Fact]
        public async Task<BookDto> Ok_WhenCalled_Get_With_Id()
        {
            var list = await Ok_WhenCalled_Get_With_PageControl();

            var model = list.FirstOrDefault();

            var result = await _bookController.Find(model.Id);

            var okResult = (OkObjectResult)result.Result;
            Assert.True(okResult.Value is BookDto);

            return (BookDto)okResult.Value;
        }

        [Fact]
        public async Task NotFound_WhenCalled_Get_Wrong_Id()
        {
            var result = await _bookController.Find(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async Task Ok_WhenCalled_Put_Without_Cover()
        {
            var model = await Ok_WhenCalled_Get_With_Id();
            model.Image = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateViewModel>(temp);

            var result = await _bookController.Put(updateModel);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task Ok_WhenCalled_Put_With_Cover()
        {
            var model = await Ok_WhenCalled_Get_With_Id();
            model.Image = "http://localhost/imagem.jpg";

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateViewModel>(temp);

            var result = await _bookController.Put(updateModel);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task BadRequest_WhenCalled_Put_Without_Title()
        {
            var model = await Ok_WhenCalled_Get_With_Id();
            model.Title = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateViewModel>(temp);

            var result = await _bookController.Put(updateModel);

            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Title"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task BadRequest_WhenCalled_Put_Without_Description()
        {
            var model = await Ok_WhenCalled_Get_With_Id();
            model.Description = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateViewModel>(temp);

            var result = await _bookController.Put(updateModel);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Description"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task BadRequest_WhenCalled_Put_Without_Author()
        {
            var model = await Ok_WhenCalled_Get_With_Id();
            model.Author = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateViewModel>(temp);

            var result = await _bookController.Put(updateModel);

            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Author"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
        }

        [Fact]
        public async Task Ok_WhenCalled_Delete()
        {
            var list = await Ok_WhenCalled_Get_With_PageControl();

            var model = list.FirstOrDefault();

            var result = _bookController.Remove(model.Id);
            Assert.True(result is OkResult);
            DeleteAllRegisters();
        }

        private async Task DeleteAllRegisters()
        {
            var list = await Ok_WhenCalled_Get_With_PageControl();
            list.ForEach(item => _bookController.Remove(item.Id));
        }
    }
}