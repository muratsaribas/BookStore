using AutoMapper;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBookCommand.CreateBookCommand;
using static WebApi.BookOperations.GetById.GetById;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModelID>().
                ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().
                ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }

    }
}