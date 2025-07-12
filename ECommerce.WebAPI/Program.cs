using MongoDB.Driver;
using ECommerce.Application.Configuration;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure MongoDB
var mongoDbConfig = builder.Configuration.GetSection("MongoDb").Get<MongoDbConfiguration>() 
    ?? new MongoDbConfiguration();

var mongoClient = new MongoClient(mongoDbConfig.ConnectionString);
var database = mongoClient.GetDatabase(mongoDbConfig.DatabaseName);

// Register MongoDB services
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddSingleton<MongoDbService>();

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllers();
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
