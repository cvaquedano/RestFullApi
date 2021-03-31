using AutoMapper;
using RestApi.Entities;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
               ;

            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<AuthorForUpdateDto, Author>();
            //CreateMap<AuthorForCreationWithDateOfDeathDto, Author>();
            //CreateMap<Author, AuthorFullDto>();
        }
    }
}
