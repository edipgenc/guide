using FluentValidation;
using Guide.Book.Application.Dto.ContactsDto;
using Guide.Book.Application.Dto.ContactsDto.PersonsDto;
using Guide.Book.Application.Helper.Validations;
using Guide.Book.Data;
using Guide.Book.Data.Repositories;
using Guide.Book.Data.Repositories.Interfaces;
using Guide.Book.Data.Services;
using Guide.Book.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IPersonService, PersonService>();

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
