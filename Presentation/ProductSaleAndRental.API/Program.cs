using Application.Helpers.Configurations;
using Application.Helpers.Middlewares;
using Marketplace.Persistence.Migrations;
using NLog.Web;
using Persistence.Configuration;
using Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfigurationApplicationLayer();
builder.Services.AddConfigurationRepositories();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigurationMigration();
builder.Host.ConfigureLogging(logging => { logging.ClearProviders(); }).UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
DatabaseCreate.Create(app.Services.GetRequiredService<IConfiguration>());
app.UseSwagger();
app.UseSwaggerUI();
//app.MigrateUpDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMIddleware>();

app.MapControllers();

app.Run();