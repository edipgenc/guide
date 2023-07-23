using AutoMapper;
using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
using Guide.Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Helper.Mappers
{
    public class PersonMapping : Profile
    {
        public PersonMapping()
        {
            CreateMap<CreatePersonDto,Person> ().ReverseMap();
        }        
    }
}
