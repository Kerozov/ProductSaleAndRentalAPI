using Marketplace.Persistence.Migrations;
using Persistence.Configuration;
using Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigurationMigration();

var app = builder.Build();

// Configure the HTTP request pipeline.
DatabaseCreate.Create(app.Services.GetRequiredService<IConfiguration>());
app.UseSwagger();
app.UseSwaggerUI();
app.MigrateUpDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();