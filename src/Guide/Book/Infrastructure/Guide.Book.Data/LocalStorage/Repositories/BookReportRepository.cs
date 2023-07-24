
using Guide.Book.Data;
using Guide.Book.Data.LocalStorage.Repositories.Interfaces;
using Guide.Book.Domain.Entities;
using MongoDB.Driver;

namespace Guide.Book.Data.LocalStorage.Repositories
{
    public class BookReportRepository : IBookReportRepository
    {
        private readonly IBookReportContext _context;
        public BookReportRepository(IBookReportContext context)
        {
            _context = context;
        }

        public async Task<ReportEntity> GetReportById(string id)
        {
            return await _context.Reports.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(ReportEntity report)
        {
            var updateResult = await _context.Reports.ReplaceOneAsync(filter:g=>g.Id==report.Id,replacement:report);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
