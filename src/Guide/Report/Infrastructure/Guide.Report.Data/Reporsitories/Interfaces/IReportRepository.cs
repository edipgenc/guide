
using Guide.Report.Domain.Entities;

namespace Guide.Report.Data.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportEntity>> GetReports();
        Task<ReportEntity> GetReportById(string id);
        Task<IEnumerable<ReportDetailEntity>> GetReportDetail(string id);
        Task<ReportEntity> Create(ReportEntity report);
        Task<bool> Update(ReportEntity report);
        Task<bool> Delete(string id);

    }
}
