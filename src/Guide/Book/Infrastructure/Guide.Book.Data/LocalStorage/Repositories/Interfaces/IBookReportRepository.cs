
using Guide.Book.Domain.Entities;

namespace Guide.Book.Data.LocalStorage.Repositories.Interfaces
{
    public interface IBookReportRepository
    {
        Task<ReportEntity> GetReportById(string id);
        Task<bool> Update(ReportEntity report);
    }
}
