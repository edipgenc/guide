using MongoDB.Driver;
using Guide.Book.Domain.Entities;

namespace Guide.Book.Data
{
    public interface IBookReportContext
    {
        IMongoCollection<ReportEntity> Reports { get;}
    }
}
