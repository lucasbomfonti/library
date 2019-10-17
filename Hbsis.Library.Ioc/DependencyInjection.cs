using Hbsis.Library.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Hbsis.Library.Ioc
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            //services.AddScoped<IBookService, BookService>();
            //services.AddScoped<IBookRepository, BookRepository>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<DataContext>();
        }
    }
}