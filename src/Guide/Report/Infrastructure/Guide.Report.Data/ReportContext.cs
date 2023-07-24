using Guide.Report.Application.Settings;
using Guide.Report.Domain.Entities;
using MongoDB.Driver;

namespace Guide.Report.Data
{
    public class ReportContext : IReportContext
    {
        // Mongo Db Connection settings
        public ReportContext(IReportDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Reports = database.GetCollection<ReportEntity>("Report");
        }
        public IMongoCollection<ReportEntity> Reports { get; }
     
    }
}
