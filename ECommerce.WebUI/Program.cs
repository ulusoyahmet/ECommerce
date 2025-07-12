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

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
