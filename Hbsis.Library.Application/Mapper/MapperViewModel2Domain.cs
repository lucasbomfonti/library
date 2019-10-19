using AutoMapper;
using Hbsis.Library.CrossCutting.Interop.ViewModel.Book;
using Hbsis.Library.Domain;

namespace Hbsis.Library.Application.Mapper
{
    public class MapperViewModel2Domain : Profile
    {
        public MapperViewModel2Domain()
        {
            CreateMap<BookInsertViewModel, Book>();
            CreateMap<BookUpdateViewModel, Book>();
        }
    }
}