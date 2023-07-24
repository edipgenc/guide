namespace Guide.Report.Application.Settings
{
    public class ReportDatabaseSettings : IReportDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
