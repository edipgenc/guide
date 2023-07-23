using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Dto.ContactsDto.PersonsDto
{
    public  class UpdatePersonDto : CreatePersonDto
    {
        public string Id { get; set; }
    }
}
