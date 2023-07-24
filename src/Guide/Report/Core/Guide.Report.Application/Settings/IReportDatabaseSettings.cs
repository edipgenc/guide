namespace Guide.Report.Application.Settings
{
    public interface IReportDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
