namespace Guide.Book.Application.Settings
{
    public class BookDatabaseSettings : IBookDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
