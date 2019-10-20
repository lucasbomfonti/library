using Hbsis.Library.Application;
using Hbsis.Library.Application.Contracts;
using Hbsis.Library.Business.Service;
using Hbsis.Library.Business.Service.Contracts;
using Hbsis.Library.Data.Context;
using Hbsis.Library.Data.Repository;
using Hbsis.Library.Data.Repository.Contracts;
using Hbsis.Library.Data.RepositoryReadOnly;
using Hbsis.Library.Data.RepositoryReadOnly.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Hbsis.Library.Ioc
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookApplication, BookApplication>();
            services.AddScoped<IBookRepositoryReadOnly, BookRepositoryReadOnly>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IUserRepositoryReadOnly, UserRepositoryReadOnly>();
            services.AddScoped<DataContext>();
        }
    }
}