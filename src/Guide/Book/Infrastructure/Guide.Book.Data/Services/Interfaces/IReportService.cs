using Guide.Book.Application.Dto.ReportDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Services.Interfaces
{
    public interface IReportService
    {
        Task<CalculateReportByLocationDto> CalculateCountReportByLocation(string location);
    }
}
