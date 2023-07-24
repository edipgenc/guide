
using Guide.Report.Data;
using Guide.Report.Data.Repositories.Interfaces;
using Guide.Report.Domain.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IReportContext _context;
        public ReportRepository(IReportContext context)
        {
            _context = context;
        }

        public  async Task<ReportEntity> Create(ReportEntity report)
        {
            await _context.Reports.InsertOneAsync(report);
            return report;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<ReportEntity>.Filter.Eq(m => m.Id,id);

            DeleteResult deleteResult = await _context.Reports.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<ReportEntity> GetReportById(string id)
        {
            return await _context.Reports.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ReportDetailEntity>> GetReportDetail(string id)
        {
            var data = await _context.Reports.Find(p => p.Id == id).FirstOrDefaultAsync();
            return data.Details.AsEnumerable();
        }

        public async Task<IEnumerable<ReportEntity>> GetReports()
        {
            return await _context.Reports.Find(p => true).ToListAsync();
        }

        public async Task<bool> Update(ReportEntity report)
        {
            var updateResult = await _context.Reports.ReplaceOneAsync(filter:g=>g.Id==report.Id,replacement:report);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
