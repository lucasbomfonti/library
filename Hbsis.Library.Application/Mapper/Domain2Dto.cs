using AutoMapper;
using Hbsis.Library.CrossCutting.Interop.Dto.Book;
using Hbsis.Library.CrossCutting.Interop.Dto.User;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application.Mapper
{
    public class Domain2Dto : Profile
    {
        public Domain2Dto()
        {
            CreateMap<Book, BookDto>();
            CreateMap<User, UserDto>();
        }
    }
}