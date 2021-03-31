using AutoMapper;
using RestApi.Entities;
using RestApi.Models;

namespace RestApi.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Book, BookForUpdateDto>();

            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}
