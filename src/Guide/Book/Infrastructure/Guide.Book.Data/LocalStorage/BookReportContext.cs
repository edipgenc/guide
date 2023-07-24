using Guide.Book.Application.Settings;
using Guide.Book.Domain.Entities;
using MongoDB.Driver;

namespace Guide.Book.Data
{
    public class BookReportContext : IBookReportContext
    {
        // Mongo Db Connection settings
        public BookReportContext(IBookDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Reports = database.GetCollection<ReportEntity>("Report");
        }
        public IMongoCollection<ReportEntity> Reports { get; }
     
    }
}
