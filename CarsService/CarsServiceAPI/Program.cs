using CarsServiceAPI.IServices;
using CarsServiceAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICarsService, CarsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseHttpMetrics();

app.UseAuthorization();

app.MapControllers();

const string MONGODB_URL = "mongodb://mongodb-cars-0.mongodb-cars-headless.default.svc.cluster.local:27017";


MongoClient mongoClient = new MongoClient(MONGODB_URL);
var carsDatabase = mongoClient.GetDatabase("carsDatabase");
var carsCollection = carsDatabase.GetCollection<BsonDocument>("cars");
if (carsCollection == null)
{
    Console.WriteLine("Collection doesn't exist...");
    await carsDatabase.CreateCollectionAsync("cars");
    carsCollection = carsDatabase.GetCollection<BsonDocument>("cars");

    await PopulateCarsDatabase(carsCollection);
}
else
{
    var documentCount = await carsCollection.CountDocumentsAsync(new BsonDocument());

    if (documentCount == 0)
        await PopulateCarsDatabase(carsCollection);
}

async Task PopulateCarsDatabase(IMongoCollection<BsonDocument> carsCollection)
{
    var documents = new List<BsonDocument>();

    var car1Object = new List<BsonElement>()
    {
        new BsonElement("carId", 0),
        new BsonElement("manufacturer", "Ford"),
        new BsonElement("model", "Focus"),
        new BsonElement("year", 2002)
    };
    var car1 = new BsonDocument(car1Object);
    var car2Object = new List<BsonElement>()
    {
        new BsonElement("carId", 1),
        new BsonElement("manufacturer", "Alfa Romeo"),
        new BsonElement("model", "GiuliaQV"),
        new BsonElement("year", 2018)
    };
    var car2 = new BsonDocument(car2Object);

    var car3Object = new List<BsonElement>()
    {
        new BsonElement("carId", 2),
        new BsonElement("manufacturer", "Fiat"),
        new BsonElement("model", "Punto"),
        new BsonElement("year", 2001)
    };
    var car3 = new BsonDocument(car3Object);

    documents.Add(car1);
    documents.Add(car2);
    documents.Add(car3);
    await carsCollection.InsertManyAsync(documents);
}

app.UseEndpoints(endpoints => {
    endpoints.MapMetrics();
});

app.Run();
