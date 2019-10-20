using AutoMapper;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.CrossCutting.Interop.ViewModel.User;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application.Mapper
{
    public class MapperViewModel2Domain : Profile
    {
        public MapperViewModel2Domain()
        {
            CreateMap<BookInsertViewModel, Book>();
            CreateMap<BookUpdateViewModel, Book>();

            CreateMap<UserInsertViewModel, User>();
            CreateMap<UserUpdateViewModel, User>();
        }
    }
}