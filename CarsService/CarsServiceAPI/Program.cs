using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

const string MONGODB_URL = "mongodb://mongodb-cars-0.mongodb-cars-headless.default.svc.cluster.local:27017";
try
{
    MongoClient mongoClient = new MongoClient(MONGODB_URL);
    var defectsDatabase = mongoClient.GetDatabase("carsDatabase");
    var carsCollection = defectsDatabase.GetCollection<BsonDocument>("cars");
    if(carsCollection == null)
    {
        Console.WriteLine("Collection doesn't exist...");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
