namespace Guide.Book.Application.Settings
{
    public interface IBookDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
