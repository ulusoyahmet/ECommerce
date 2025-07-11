using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Connection String (from your appsettings.json or environment variables)
const string connectionString = "mongodb://localhost:27017";

// 2. Create a Client
var mongoClient = new MongoClient(connectionString);

// 3. Get a Database Reference
IMongoDatabase database = mongoClient.GetDatabase("ECommerceDb");

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
