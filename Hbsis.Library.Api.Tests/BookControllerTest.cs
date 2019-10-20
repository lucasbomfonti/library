using Hbsis.Library.Api.Controllers;
using Hbsis.Library.Api.Tests.Helper;
using Hbsis.Library.Application;
using Hbsis.Library.Application.Mapper;
using Hbsis.Library.Business.Service;
using Hbsis.Library.CrossCutting;
using Hbsis.Library.CrossCutting.Exceptions;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository;
using Hbsis.Library.Data.RepositoryReadOnly;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Hbsis.Library.Api.Tests
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

        [Test]
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
            Assert.True(result is ObjectResult);
        }

        [Test]
        public void Ok_WhenCalled_Post_Without_Cover()
        {
            var model = new BookInsertViewModel
            {
                Title = "Book 1",
                Description = "There is a book 1",
                Author = "Author 1",
                Image = null
            };

            _bookController.ModelState.AddModelError("Image", "required");

            Assert.ThrowsAsync<EntityValidationException>(async () => await _bookController.Post(model));
        }
    }
}