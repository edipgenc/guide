using Guide.Book.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Dto.ReportDto
{
    public class ReportDto : BaseEntity
    {
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
