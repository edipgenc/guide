using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Application.Dto.ReportDto
{
    public class CalculateReportByLocationDto
    {
        public string Location { get; set; }
        public string ReportId { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
