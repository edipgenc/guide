using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Dto.ContactsDto
{
    public class UpdateContactDto :  CreateContactDto
    {
        public string Id { get; set; }
    }
}
