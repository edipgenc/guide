using Guide.Book.Domain.Common;
using Guide.Book.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Guid PersonId { get; set; }
        public TInfoType InfoType { get; set; }
        [Required]
        public string Content { get; set; }
        public virtual Person Person { get; set; }
    }
}
