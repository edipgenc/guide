using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using FluentValidation;
using Guide.Book.Api.Extentions;
using Guide.Book.Application.Dto.ContactsDto;
using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
using Guide.Book.Application.Helper.Validations;
using Guide.Book.Application.Settings;
using Guide.Book.Data;
using Guide.Book.Data.LocalStorage.Repositories;
using Guide.Book.Data.LocalStorage.Repositories.Interfaces;
using Guide.Book.Data.Repositories;
using Guide.Book.Data.Repositories.Interfaces;
using Guide.Book.Data.Services;
using Guide.Book.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var config = builder.Configuration;

#region Configuration Dependencies
// here is the mongo config file and parameters settings
builder.Services.Configure<BookDatabaseSettings>(builder.Configuration.GetSection(nameof(BookDatabaseSettings)));
builder.Services.AddSingleton<IBookDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BookDatabaseSettings>>().Value);
#endregion

#region Project Dependencies
// here is the mongo database and parameters settings
builder.Services.AddTransient<IBookReportContext, BookReportContext>();
builder.Services.AddTransient<IBookReportRepository, BookReportRepository>();

#endregion


#region Configration Dependencies
// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Fluent validator manualy
builder.Services.AddTransient<IValidator<CreatePersonDto>, PersonValidator>();
builder.Services.AddTransient<IValidator<CreateContactDto>, ContactValidator>();

builder.Services.AddTransient<IValidator<UpdatePersonDto>, PersonUpdateValidator>();
builder.Services.AddTransient<IValidator<UpdateContactDto>, ContactUpdateValidator>();

#endregion

#region Project dependecies 
builder.Services.AddDbContext<ApplicationContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("GuideData"), b => b.MigrationsAssembly("Guide.Book.Api")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IReportService, ReportService>();



#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region EventBus
//  RabbitMQ Connection
builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
    var factory = new ConnectionFactory()
    {
        HostName = config["EventBus:HostName"]
    };
    if (!string.IsNullOrWhiteSpace(config["EventBus:UserName"]))
    {
        factory.UserName = config["EventBus:UserName"];
    }
    if (!string.IsNullOrWhiteSpace(config["EventBus:Password"]))
    {
        factory.Password = config["EventBus:Password"];
    }
    var RetryCount = 5;
    if (!string.IsNullOrWhiteSpace(config["EventBus:RetryCount"]))
    {
        RetryCount = int.Parse(config["EventBus:RetryCount"]);
    }
    return new DefaultRabbitMQPersistentConnection(factory, RetryCount, logger);

});

// RabbitMQ Event Bus
builder.Services.AddSingleton<EventBusRabbitMQProducer>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRabbitListener();

app.Run();
