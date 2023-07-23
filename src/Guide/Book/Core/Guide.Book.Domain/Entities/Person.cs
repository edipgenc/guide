using Guide.Book.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Domain.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Company { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
