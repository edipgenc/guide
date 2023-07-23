using Guide.Book.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Dto.ContactsDto
{
    public class CreateContactDto
    {
        public string PersonId { get; set; }
        public TInfoType InfoType { get; set; }
        public string Content { get; set; }
    }
}
