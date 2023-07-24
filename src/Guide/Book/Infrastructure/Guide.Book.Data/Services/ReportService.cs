using Guide.Book.Application.Dto.ReportDto;
using Guide.Book.Data.Repositories.Interfaces;
using Guide.Book.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Services
{
    public class ReportService : IReportService
    {

        private readonly ILogger<ReportService> _logger;
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository, ILogger<ReportService> logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }
        public async Task<CalculateReportByLocationDto> CalculateCountReportByLocation(string location)
        {

            string sqlQuery = String.Format(@"Select 
                            (Select count(*) as PersonCount from public.\""Contacts\"" as Contact
                            inner join public.\""Persons\"" as Person On Person.\""Id\"" = Contact.\""PersonId\""
                            where Contact.\""InfoType\"" = 3 and Contact.\""Content\"" = '{0}'),
	
                            (Select count(*) as PhoneCount from public.\""Contacts\"" as Contact
                            inner join public.\""Persons\"" as Person On Person.\""Id\"" = Contact.\""PersonId\""
                            inner join  public.\""Contacts\"" as ContactPhone on Person.\""Id\"" = ContactPhone.\""PersonId\""
                            and ContactPhone.\""InfoType\""= 2 and Contact.\""Content\"" is not null
                             where Contact.\""InfoType\"" = 3 and Contact.\""Content\"" = '{0}'
                            )   ", location);



            var _executingTask = _reportRepository.GetSql(sqlQuery).ToList();
            CalculateReportByLocationDto data = new CalculateReportByLocationDto()
            {
                
                Location = location,
                PersonCount = Convert.ToInt32(_executingTask[0].PersonCount),
                PhoneCount = Convert.ToInt32(_executingTask[0].PhoneCount)
            };
            return data;

        }


    }
}
