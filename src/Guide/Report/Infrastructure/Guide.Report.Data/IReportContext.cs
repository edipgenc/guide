using MongoDB.Driver;
using Guide.Report.Domain.Entities;

namespace Guide.Report.Data
{
    public interface IReportContext
    {
        IMongoCollection<ReportEntity> Reports { get;}
    }
}
