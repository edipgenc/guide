using ESourcing.Products.Repositories;
 
using Guide.Report.Application.Settings;
using Guide.Report.Data;
using Guide.Report.Data.Repositories.Interfaces;

using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var config = builder.Configuration;
 


#region Configuration Dependencies
// here is the mongo config file and parameters settings
builder.Services.Configure<ReportDatabaseSettings>(builder.Configuration.GetSection(nameof(ReportDatabaseSettings)));
builder.Services.AddSingleton<IReportDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ReportDatabaseSettings>>().Value);
#endregion



#region Project Dependencies
// here is the mongo database and parameters settings
builder.Services.AddTransient<IReportContext, ReportContext>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();

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

app.Run();
