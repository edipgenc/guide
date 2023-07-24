using Guide.Book.Application.Dto.ReportDto;
using Guide.Book.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Repositories
{
    public class ReportRepository : GenericRepository<ReportDto>, IReportRepository
    {
        private readonly ApplicationContext _dbContext;
        public ReportRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}